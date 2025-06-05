using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Models;

namespace NotifyAPI.Contexts
{
    public class NotifyDBContext : DbContext
    {
        public NotifyDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasOne(u => u.User)
                                            .WithOne(ep => ep.Employee)
                                            .HasForeignKey<Employee>(ep => ep.Email)
                                            .HasConstraintName("FK_Employee_User")
                                            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Document>().HasOne(d => d.UploadedBy)
                                            .WithMany()
                                            .HasForeignKey(up => up.UploadedById);
        }
    }
}