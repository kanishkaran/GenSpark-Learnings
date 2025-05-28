using System;
using HealthAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthAPI.Contexts;

public class HealthCareDbContext : DbContext
{
    public HealthCareDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Patient> patients { get; set; }
    public DbSet<Doctor> doctors { get; set; }
    public DbSet<Appointment> appointments { get; set; }
    public DbSet<Specialization> specializations { get; set; }
    public DbSet<DoctorSpecialization> doctorSpecializations { get; set; }

    // public DbSet<User> users { get; set; }
    // public DbSet<UserFollow> userFollows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<User>().HasKey(u => u.UserId);

        // modelBuilder.Entity<UserFollow>().HasKey(uf => new {uf.FollowerId, uf.FollowingId});

        // modelBuilder.Entity<UserFollow>().HasOne(uf => uf.Follower)
        //                                 .WithMany(u => u.Followings)
        //                                 .HasForeignKey(uf => uf.FollowerId)
        //                                 .HasConstraintName("FK_Follower_Follow")
        //                                 .OnDelete(DeleteBehavior.Restrict);

        // modelBuilder.Entity<UserFollow>().HasOne(uf => uf.Following)
        //                                 .WithMany(u => u.Followers)
        //                                 .HasForeignKey(uf => uf.FollowingId)
        //                                 .HasConstraintName("FK_following_follow")
        //                                 .OnDelete(DeleteBehavior.Restrict);
                                

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
