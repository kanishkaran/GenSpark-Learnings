using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Misc
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<PatientAddRequestDto, Patient>();

            
        }
    }
}