using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Misc
{
    public class DoctorMapper
    {
        public Doctor MapDoctorAddRequest(DoctorAddRequestDto doctor)
        {
            Doctor newDoctor = new();
            newDoctor.DoctorName = doctor.Name;
            newDoctor.Status = "Active";
            newDoctor.Email = doctor.Email;
            newDoctor.YearOfExp = doctor.YearOfExp;
            return newDoctor;
        }
    }
}