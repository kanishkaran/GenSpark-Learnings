
using System.Collections.Generic;
using HealthAPI.Interfaces;
using HealthAPI.Models;
using HealthAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HealthAPI.Controllers
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
        public async Task<ActionResult<ICollection<Doctor>>> GetDoctorsBySpeciality(string speciality)
        {
            return Ok(await _doctorService.GetDoctorsBySpeciality(speciality));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]
        public async Task<ActionResult<DoctorResponseDto>> AddDoctors([FromBody] DoctorAddRequestDto doctor)
        {
            var doc = await _doctorService.AddDoctor(doctor);
            var result = new DoctorResponseDto
            {
                Id = doc.Id,
                DoctorName = doc.DoctorName,
                Status = doc.Status,
                Specializations = doc.doctorSpecializations?
                                .Select(ds => ds.Specialization?.Name ?? string.Empty)
                                .Where(name => !string.IsNullOrEmpty(name))
                                .ToList() ?? new List<string>()
            };
            return Created("", result);
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