using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebWorker.contexts
{
    public class WebWorkerDBContext : DbContext
    {
        public WebWorkerDBContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}