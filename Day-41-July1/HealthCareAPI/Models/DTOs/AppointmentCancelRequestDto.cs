using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Models.DTOs
{
    public class AppointmentCancelRequestDto
    {
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
    }
}