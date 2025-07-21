using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using VideoStreamingApp.contexts;
using VideoStreamingApp.interfaces;
using VideoStreamingApp.repositories;
using VideoStreamingApp.services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
#region db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region repository
builder.Services.AddScoped<ITrainingVideoRepository, TrainingVideoRepository>();
#endregion

#region service 
builder.Services.AddScoped<ITrainingVideoService, TrainingVideoService>();
#endregion
builder.Services.AddCors(options =>
{
    options.AddPolicy("angular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


builder.Services.AddSingleton(new BlobContainerClient(new Uri(builder.Configuration["AzureBlobs:ContainerSasUrl"])));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("angular");
app.MapControllers();

app.Run();
