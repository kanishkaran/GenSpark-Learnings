

namespace HealthCareAPI.Models;

public class DoctorSpecialization
{
        public int SerialNumber { get; set; }
        public int DoctorId { get; set; }
        public int SpecializationId { get; set; }

        public Specialization? Specialization { get; set; }
        public Doctor? Doctor { get; set; }
}
