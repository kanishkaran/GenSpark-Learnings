

namespace HealthCareAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string DoctorName { get; set; } = string.Empty;
        public string Specialization { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public ICollection<Appointment>? appointments;

        public ICollection<DoctorSpecialization>? doctorSpecializations;
    }
}