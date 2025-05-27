
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HealthCareAPI.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
        [ForeignKey("PatientId")]
        public Patient? patient { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? doctor { get; set; }
    }
}