using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Interfaces
{
    public interface IPatientService
    {
        Task<Patient> RegisterPatient(PatientAddRequestDto patient);

    }
}