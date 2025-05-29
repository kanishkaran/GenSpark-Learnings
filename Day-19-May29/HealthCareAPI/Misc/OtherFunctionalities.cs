using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Misc
{
    public class OtherFunctionalities : IOtherFunctionalities
    {
        private readonly HealthCareDbContext _context;

        public OtherFunctionalities(HealthCareDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<DoctorBySpecialityDto>> GetDoctorsBySpeciality(string speciality)
        {
            return await _context.GetDoctorBySpeciality(speciality);
        }
    }
}