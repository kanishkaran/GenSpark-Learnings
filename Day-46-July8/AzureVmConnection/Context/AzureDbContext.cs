using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.models;
using Microsoft.EntityFrameworkCore;

namespace AzureVmConnection.Context
{
    public class AzureDbContext : DbContext
    {
        public AzureDbContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<UserTest> UserTests { get; set; }
    }
}