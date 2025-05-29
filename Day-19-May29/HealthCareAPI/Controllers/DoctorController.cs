
using System.Collections.Generic;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthCareAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Doctor>), StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<Doctor>>> GetDoctors()
        {
            return Ok(await _doctorService.GetAllDoctors());
        }

        [HttpGet("{speciality}")]
        public async Task<ActionResult<ICollection<DoctorBySpecialityDto>>> GetDoctorsBySpeciality(string speciality)
        {
            return Ok(await _doctorService.GetDoctorsBySpeciality(speciality));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]
        public async Task<ActionResult<DoctorResponseDto>> AddDoctors([FromBody] DoctorAddRequestDto doctor)
        {
            try
            {
                var newDoctor = await _doctorService.AddDoctor(doctor);
                if (newDoctor != null)
                    return Created("", newDoctor);
                return BadRequest("cannot  process the request at this moment");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // [HttpPut]
        // public ActionResult<Doctor> UpdateDoctor([FromBody] Doctor updated_doctor)
        // {
        //     int id = updated_doctor.Id;

        //     var doctor = doctorList.FirstOrDefault(doc => doc.Id == id);

        //     if (doctor == null)
        //         return NotFound();

        //     doctor.DoctorName = updated_doctor.DoctorName;
        //     return Ok(doctor);
        // }

        // [HttpDelete]
        // public ActionResult<List<Doctor>> DeleteDoctor([FromBody] int id)
        // {
        //     var doctor = doctorList.FirstOrDefault(doc => doc.Id == id);

        //     if (doctor == null)
        //         return NotFound();

        //     doctorList.Remove(doctor);
        //     return doctorList;

        // }

    }
}