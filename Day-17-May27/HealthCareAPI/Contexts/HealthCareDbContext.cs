using System;
using HealthCareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Contexts;

public class HealthCareDbContext : DbContext
{
    public HealthCareDbContext(DbContextOptions options) : base(options) { }

    DbSet<Patient> patients { get; set; }
    DbSet<Doctor> doctors { get; set; }
    DbSet<Appointment> appointments { get; set; }
    DbSet<Specialization> specializations { get; set; }
    DbSet<DoctorSpecialization> doctorSpecializations { get; set; }
}
