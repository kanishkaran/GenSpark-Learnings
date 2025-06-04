
using HealthCareAPI.Models;
using HealthCareAPI.Contexts;
using Microsoft.EntityFrameworkCore;
using HealthCareAPI.Exceptions;

namespace HealthCareAPI.Repositories
{
    public class PatientRepository : Repository<int, Patient>
    {
        public PatientRepository(HealthCareDbContext context) : base(context) {}

        public override async Task<IEnumerable<Patient>> GetAll()
        {
            var patients = await _context.patients.ToListAsync();
            return patients.Count == 0 ? throw new CollectionEmptyException("No patients Found") : patients;
        }

        public override async Task<Patient> GetById(int id)
        {
            var patient = await _context.patients.SingleOrDefaultAsync(p => p.Id == id);
            return patient ?? throw new KeyNotFoundException("Patient Not Found");
        }

    }
}