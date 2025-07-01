

namespace HealthCareAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public ICollection<Appointment>? appointments { get; set; }

        public User? User { get; set; }

    }
}