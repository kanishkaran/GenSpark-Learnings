
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        static List<Patient> patientList = new List<Patient>
        {
            new Patient{Id = 1, PatientName= "Ollie", Age= 22},
            new Patient{Id = 2, PatientName = "Lucas", Age = 21}
        };

        [HttpGet]
        [ProducesResponseType(typeof(List<Patient>), StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(patientList);
        }


        [HttpPost]
        [ProducesResponseType(typeof(Patient), StatusCodes.Status201Created)]
        public ActionResult<List<Patient>> AddPatients([FromBody] AddPatient patient)
        {
            Patient new_patient = new Patient { Id = GenerateId(), Age = patient.Age, PatientName = patient.PatientName };
            patientList.Add(new_patient);
            return Created("", patientList);
        }

        private int GenerateId()
        {
            if (patientList == null)
            {
                return 1;
            }

            return patientList.Max(e => e.Id) + 1;
        }

        [HttpPut]
        public ActionResult<Patient> UpdatePatient([FromBody] Patient updated_patient)
        {
            int id = updated_patient.Id;

            var patient = patientList.FirstOrDefault(doc => doc.Id == id);

            if (patient == null)
                return NotFound();

            patient.PatientName = updated_patient.PatientName;
            patient.Age = updated_patient.Age;
            return Ok(patient);
        }

        [HttpDelete]
        public ActionResult<List<Patient>> DeletePatient([FromBody] int id)
        {
            var patient = patientList.FirstOrDefault(doc => doc.Id == id);

            if (patient == null)
                return NotFound();

            patientList.Remove(patient);
            return patientList;

        }
    }
}