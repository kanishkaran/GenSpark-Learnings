using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;
        private readonly ILogger _logger;

        public AuthenticationService(IRepository<string, User> userRepository,
                                    IEncryptionService encryptionService,
                                    ITokenService tokenService,
                                    ILogger<AuthenticationService> logger)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
            _logger = logger;
        }
        public async Task<UserLoginResponse> Login(UserLoginRequest user)
        {
            var dbUser = await _userRepository.GetById(user.Username);

            if (dbUser == null)
            {
                _logger.LogCritical("User not found");
                throw new Exception("User not found");
            }

            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = user.Password,
                Hashkey = dbUser.HashKey
                
            });

            for (int i = 0; i < encryptedData.EncryptedDate.Length; i++)
            {
                if (encryptedData.EncryptedDate[i] != dbUser.Password[i])
                {
                    _logger.LogError("Invalid login attempt");
                    throw new Exception("Invalid password");
                }

            }
            var token = await _tokenService.GenerateToken(dbUser);
            return new UserLoginResponse
            {
                Username = user.Username,
                Token = token,
            };
        }
    }
}