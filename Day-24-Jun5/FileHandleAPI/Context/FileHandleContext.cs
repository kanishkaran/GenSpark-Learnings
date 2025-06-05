using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileHandleAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FileHandleAPI.Context
{
    public class FileHandleContext : DbContext
    {
        public FileHandleContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<FileData> Files { get; set; }
        
    }
}