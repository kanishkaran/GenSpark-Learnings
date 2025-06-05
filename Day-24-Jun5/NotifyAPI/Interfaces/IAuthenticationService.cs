using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserLoginResponse> Login(UserLoginRequest user);
    }
}