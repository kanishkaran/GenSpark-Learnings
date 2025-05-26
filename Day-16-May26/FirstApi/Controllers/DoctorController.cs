
using System.Collections.Generic;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : ControllerBase
    {
        static List<Doctor> doctorList = new List<Doctor>
        {
            new Doctor{Id = 1, DoctorName= "Alex"},
            new Doctor{Id = 2, DoctorName = "Matt"}
        };

        [HttpGet]
        [ProducesResponseType(typeof(List<Doctor>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Doctor>> GetDoctors()
        {
            return Ok(doctorList);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Doctor), StatusCodes.Status201Created)]
        public ActionResult<Doctor> AddDoctors([FromBody] Doctor doctor)
        {
            doctorList.Add(doctor);
            return Created("", doctorList);
        }

        [HttpPut]
        public ActionResult<Doctor> UpdateDoctor([FromBody] Doctor updated_doctor)
        {
            int id = updated_doctor.Id;

            var doctor = doctorList.FirstOrDefault(doc => doc.Id == id);

            if (doctor == null)
                return NotFound();

            doctor.DoctorName = updated_doctor.DoctorName;
            return Ok(doctor);
        }

        [HttpDelete]
        public ActionResult<List<Doctor>> DeleteDoctor([FromBody] int id)
        {
            var doctor = doctorList.FirstOrDefault(doc => doc.Id == id);

            if (doctor == null)
                return NotFound();

            doctorList.Remove(doctor);
            return doctorList;

        }

    }
}