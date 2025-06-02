using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;

        public PatientController(IPatientService service,
                                ILogger<PatientController> logger)
        {
            _patientService = service;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Patient>> RegisterPatient([FromBody] PatientAddRequestDto patient)
        {
            try
            {
                var newPatient = await _patientService.RegisterPatient(patient);
                if (newPatient != null)
                    return Created("", newPatient);
                return BadRequest("cannot  process the request at this moment");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}