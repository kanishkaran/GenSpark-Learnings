using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Misc;
using HealthCareAPI.Models;
using HealthCareAPI.Repositories;
using HealthCareAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });

                
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddTransient<IRepository<int, Patient>, PatientRepository>();
builder.Services.AddTransient<IRepository<int, Specialization>, SpecializationRepository>();
builder.Services.AddTransient<IRepository<int, Appointment>, AppointmentRepository>();
builder.Services.AddTransient<IRepository<int, DoctorSpecialization>, DoctorSpecializationRepository>();

builder.Services.AddTransient<IDoctorService, DoctorService>();

builder.Services.AddTransient<IOtherFunctionalities, OtherFunctionalities>();

builder.Services.AddDbContext<HealthCareDbContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    app.UseSwagger();
}

app.MapControllers();



app.Run();


