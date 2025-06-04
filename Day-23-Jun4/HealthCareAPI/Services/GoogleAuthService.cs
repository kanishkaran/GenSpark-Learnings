using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Services
{
public class GoogleAuthService : IGoogleAuthService
{
        private readonly ITokenService _tokenService;
        private readonly IRepository<string, User> _userRepo;

        public GoogleAuthService(ITokenService tokenService,
                                IRepository<string, User> repository)
        {
            _tokenService = tokenService;
            _userRepo = repository;
    }
        public async Task<UserLoginResponse> HandleGoogleLoginAsync(ClaimsPrincipal principal)
        {
            var email = principal.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = principal.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;

            var newUser = new User
            {
                Username = email,
                Role = "Doctor"
            };
            await _userRepo.Add(newUser);
            var token = await _tokenService.GenerateToken(newUser);
            return new UserLoginResponse
            {
                Token = token,
                Username = email
            };
        }
}
}