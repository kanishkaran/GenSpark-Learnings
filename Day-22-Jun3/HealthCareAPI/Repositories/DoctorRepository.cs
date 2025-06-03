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
    public class DoctorRepository : Repository<int, Doctor>
    {
        public DoctorRepository(HealthCareDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<Doctor>> GetAll()
        {
            var doctors = await _context.doctors.ToListAsync();
            return doctors.Count == 0 ? new List<Doctor>() : doctors;
        }

        public override async Task<Doctor> GetById(int id)
        {
            var doctor = await _context.doctors.SingleOrDefaultAsync(d => d.Id == id);
            return doctor ?? throw new KeyNotFoundException("Appointment not found");
        }
    }
}