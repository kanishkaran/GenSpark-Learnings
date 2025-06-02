

namespace HealthCareAPI.Models
{

    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public ICollection<DoctorSpecialization>? DoctorSpecializations { get; set; }
    }
}
