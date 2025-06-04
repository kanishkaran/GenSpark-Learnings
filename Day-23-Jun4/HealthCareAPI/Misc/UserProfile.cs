using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Misc
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<DoctorAddRequestDto, User>()
                .ForMember(dest => dest.Username, act => act.MapFrom(d => d.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<PatientAddRequestDto, User>()
                .ForMember(dest => dest.Username, act => act.MapFrom(p => p.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());
        }
    }
}