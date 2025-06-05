using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<EmployeeAddRequestDto, User>()
                .ForMember(dest => dest.UserName, act => act.MapFrom(d => d.Email))
                .ForMember(dest => dest.Password, opt => opt.Ignore());

            CreateMap<EmployeeAddRequestDto, Employee>();
        }  
    }
}