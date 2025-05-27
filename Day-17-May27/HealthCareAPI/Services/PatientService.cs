using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;
using HealthCareAPI.Interfaces;

namespace HealthCareAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;

        public PatientService(IRepository<int, Patient> repository)
        {
            _patientRepository = repository;
        }

        public int AddPatient(Patient patient)
        {
            try
            {
                var result = _patientRepository.Add(patient);
                if (result != null)
                    return result.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public int DeletePatient(int id)
        {
            try
            {
                var result = _patientRepository.Delete(id);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }

        public ICollection<Patient>? GetAllPatient()
        {
            try
            {
                var patients = _patientRepository.GetAll();
                return patients;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public Patient? GetPatientById(int id)
        {
            try
            {
                var patient = _patientRepository.GetById(id);
                return patient;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public int UpdatePatient(Patient new_patient)
        {
            try
            {
                var result = _patientRepository.Update(new_patient);
                if (result != null)
                    return result.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return -1;
        }
    }
}