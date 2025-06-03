using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IAuthenticationService authenticationService, ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserLoginResponse>> UserLogin(UserLoginRequest loginRequest)
        {
            try
            {
                var result = await _authenticationService.Login(loginRequest);
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return Unauthorized(e.Message);
            }
        }

    }
}