using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Models.DTOs
{
    public class DoctorBySpecialityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}