

namespace HealthAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string DoctorName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public ICollection<Appointment>? appointments { get; set; }
        public ICollection<DoctorSpecialization>? doctorSpecializations { get; set; }
    }
}