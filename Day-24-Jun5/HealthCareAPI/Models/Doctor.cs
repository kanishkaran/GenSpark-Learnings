

namespace HealthCareAPI.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string DoctorName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public float YearOfExp { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<DoctorSpecialization>? DoctorSpecializations { get; set; }

        public User? User { get; set; }
    }
}