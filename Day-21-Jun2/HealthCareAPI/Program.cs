using System.Text;
using AutoMapper;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Misc;
using HealthCareAPI.Models;
using HealthCareAPI.Repositories;
using HealthCareAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                    opts.JsonSerializerOptions.WriteIndented = true;
                });


#region Repositories               
builder.Services.AddTransient<IRepository<int, Doctor>, DoctorRepository>();
builder.Services.AddTransient<IRepository<int, Patient>, PatientRepository>();
builder.Services.AddTransient<IRepository<int, Specialization>, SpecializationRepository>();
builder.Services.AddTransient<IRepository<int, Appointment>, AppointmentRepository>();
builder.Services.AddTransient<IRepository<int, DoctorSpecialization>, DoctorSpecializationRepository>();
builder.Services.AddTransient<IRepository<string, User>, UserRepository>();
#endregion


#region Services
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IDoctorService, DoctorService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IPatientService, PatientService>();


builder.Services.AddTransient<IOtherFunctionalities, OtherFunctionalities>();
#endregion

#region Misc
builder.Services.AddAutoMapper(typeof(User));
#endregion




#region AuthenticationFilter
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Keys:JwtTokenKey"]))
                    };
                });
#endregion

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


