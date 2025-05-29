using System;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Contexts;

public class HealthCareDbContext : DbContext
{
    public HealthCareDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Patient> patients { get; set; }
    public DbSet<Doctor> doctors { get; set; }
    public DbSet<Appointment> appointments { get; set; }
    public DbSet<Specialization> specializations { get; set; }
    public DbSet<DoctorSpecialization> doctorSpecializations { get; set; }

    public DbSet<DoctorBySpecialityDto> doctorsBySpeciality { get; set; }


    public async Task<List<DoctorBySpecialityDto>> GetDoctorBySpeciality(string speciality)
    {
        return await this.Set<DoctorBySpecialityDto>()
                            .FromSqlInterpolated($"SELECT *  FROM func_get_doctors_by_specialization({speciality})")
                            .ToListAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {



        modelBuilder.Entity<Appointment>().HasKey(ap => ap.Id).HasName("PK_Appointment_Id");

        modelBuilder.Entity<Appointment>().HasOne(p => p.patient)
                                        .WithMany(ap => ap.appointments)
                                        .HasForeignKey(p => p.PatientId)
                                        .HasConstraintName("FK_Appointment_Patient")
                                        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>().HasOne(d => d.doctor)
                                   .WithMany(ap => ap.appointments)
                                   .HasForeignKey(d => d.DoctorId)
                                   .HasConstraintName("FK_Appointment_Doctor")
                                   .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DoctorSpecialization>().HasKey(ds => ds.SerialNumber);

        modelBuilder.Entity<DoctorSpecialization>().HasOne(ds => ds.Doctor)
                                               .WithMany(d => d.doctorSpecializations)
                                               .HasForeignKey(ds => ds.DoctorId)
                                               .HasConstraintName("FK_Specialization_Doctor")
                                               .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<DoctorSpecialization>().HasOne(ds => ds.Specialization)
                                               .WithMany(s => s.DoctorSpecializations)
                                               .HasForeignKey(ds => ds.SpecializationId)
                                               .HasConstraintName("FK_Specialization_Spec")
                                               .OnDelete(DeleteBehavior.Restrict);
    }
}
