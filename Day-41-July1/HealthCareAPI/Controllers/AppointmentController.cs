using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("Book")]
        [Authorize]
        public async Task<ActionResult<Appointment>> BookAppointment(AppointmentAddRequestDto appointment)
        {
            try
            {
                return await _appointmentService.BookAppointment(appointment);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Cancel")]
        [Authorize(Policy = "atleast3")]
        public async Task<ActionResult<Appointment>> CancelAppointment(AppointmentCancelRequestDto cancelRequest)
        {
            try
            {
                return await _appointmentService.CancelAppointment(cancelRequest);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}