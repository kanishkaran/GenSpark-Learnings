using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Misc
{
    public class SpecializationMapper
    {
        public Specialization MapSpecializationAddRequest(SpecializationAddRequestDto specialization)
        {
            Specialization newSpecialization = new();
            newSpecialization.Name = specialization.Name;
            return newSpecialization;
        }

        public DoctorSpecialization MapDoctorSpecility(int doctorId, int specialityId)
        {
            DoctorSpecialization doctorSpeciality = new();
            doctorSpeciality.DoctorId = doctorId;
            doctorSpeciality.SpecializationId = specialityId;
            return doctorSpeciality;
        }
    }
}