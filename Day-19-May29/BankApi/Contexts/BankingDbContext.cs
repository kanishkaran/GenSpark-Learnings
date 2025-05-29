using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Contexts
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<TransactionEntry> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(ac => ac.Id);

            modelBuilder.Entity<TransactionEntry>().HasKey(t => t.Id);

            modelBuilder.Entity<TransactionEntry>().HasOne(a => a.Account)
                                                    .WithMany(t => t.Transactions)
                                                    .HasForeignKey(a => a.AccountId)
                                                    .OnDelete(DeleteBehavior.Restrict);
        }
 
    }
}