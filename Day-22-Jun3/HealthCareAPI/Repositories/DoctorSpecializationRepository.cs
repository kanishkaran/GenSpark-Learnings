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
    public class DoctorSpecializationRepository : Repository<int, DoctorSpecialization>
    {
        public DoctorSpecializationRepository(HealthCareDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<DoctorSpecialization>> GetAll()
        {
            var doctorSpecializations = await _context.doctorSpecializations.ToListAsync();
            return doctorSpecializations.Count == 0 ? throw new CollectionEmptyException("No doctorSpecializations Found") : doctorSpecializations;
        }

        public override async Task<DoctorSpecialization> GetById(int id)
        {
            var doctorSpecialization = await _context.doctorSpecializations.SingleOrDefaultAsync(doc => doc.SerialNumber  == id);
            return doctorSpecialization ?? throw new KeyNotFoundException("Patient Not Found");
        }
    }
}