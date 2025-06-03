using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}