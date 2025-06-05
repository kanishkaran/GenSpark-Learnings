using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Interfaces
{
    public interface IGoogleAuthService
    {
         Task<UserLoginResponse> HandleGoogleLoginAsync(ClaimsPrincipal principal);
    }
}