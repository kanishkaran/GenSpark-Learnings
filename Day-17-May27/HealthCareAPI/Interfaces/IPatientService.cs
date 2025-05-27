using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;

namespace HealthCareAPI.Interfaces
{
    public interface IPatientService
    {
        int AddPatient(Patient patient);
        int UpdatePatient(Patient new_patient);

        Patient? GetPatientById(int id);

        ICollection<Patient>? GetAllPatient();
        int DeletePatient(int id);
    }
}