using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace HealthCareAPI.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _securityKey;
        private readonly IRepository<int, Doctor> _doctorRepository;

        public TokenService(IConfiguration configuration,
                            IRepository<int, Doctor> repository)
        {
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Keys:JwtTokenKey"]));

            _doctorRepository = repository;
        }
        public async Task<string> GenerateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Username),
                new(ClaimTypes.Role, user.Role)
            };
            var allDoctors = await _doctorRepository.GetAll();
            var doctor = allDoctors.FirstOrDefault(doc => doc.Email == user.Username);
            if (doctor != null && user.Role == "Doctor")
                claims.Add(new("YearsOfExp", doctor.YearOfExp.ToString()));

            var credential = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credential
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}