using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.DTOs;
using AzureVmConnection.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AzureVmConnection.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                var users = _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception)
            {

                return BadRequest("User not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserAddDto user)
        {
            try
            {
                var newUser = await _userService.AddUser(user);
                return Ok(newUser);
            }
            catch (System.Exception)
            {

                return BadRequest("User Not added");
            }
        }
    }
}