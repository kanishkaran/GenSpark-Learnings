


namespace HealthCareAPI.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        public Patient? patient { get; set; }
        public Doctor? doctor { get; set; }
    }
}