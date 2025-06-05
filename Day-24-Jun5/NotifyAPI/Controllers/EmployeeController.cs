using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotifyAPI.Interfaces;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Employee>> RegisterEmployee(EmployeeAddRequestDto employee)
        {
            try
            {
                var result = await _employeeService.RegisterEmployee(employee);
                return result;
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}