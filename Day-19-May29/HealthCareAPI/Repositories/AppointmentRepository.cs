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
    public class AppointmentRepository : Repository<int, Appointment>
    {
        public AppointmentRepository(HealthCareDbContext context) : base(context)
        {
            
        }
        public override async Task<IEnumerable<Appointment>> GetAll()
        {
            var appointments = await _context.appointments.ToListAsync();
            return appointments.Count == 0 ? throw new CollectionEmptyException("No Appointments in DB") : appointments;
        }

        public override async Task<Appointment> GetById(int id)
        {
            var appointment = await _context.appointments.SingleOrDefaultAsync(ap => ap.Id == id);
            return appointment ?? throw new KeyNotFoundException("Appointment not found");
        }
    }
}