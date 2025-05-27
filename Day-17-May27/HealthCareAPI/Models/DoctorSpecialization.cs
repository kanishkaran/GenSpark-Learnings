using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthCareAPI.Models;

public class DoctorSpecialization
{
        [Key]
        public int SerialNumber { get; set; }
        public int DoctorId { get; set; }
        public int SpecializationId { get; set; }

        [ForeignKey("SpecializationId")]
        public Specialization? Specialization { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor? Doctor { get; set; }
}
