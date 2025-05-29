using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Contexts;
using HealthCareAPI.Exceptions;
using HealthCareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Repositories
{
    public class SpecializationRepository : Repository<int, Specialization>
    {
        public SpecializationRepository(HealthCareDbContext context) : base(context)
        {
            
        }
        public override async Task<IEnumerable<Specialization>> GetAll()
        {
            var specializations = await _context.specializations.ToListAsync();
            return specializations.Count == 0 ? throw new CollectionEmptyException("No specializations Found") : specializations;
        }

        public override async Task<Specialization> GetById(int id)
        {
            var specialization = await _context.specializations.SingleOrDefaultAsync(sp => sp.Id == id);
            return specialization ?? throw new KeyNotFoundException("Specialization Not Found");
        }
    }
}