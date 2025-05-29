using System;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Interfaces;

public interface IDoctorService
{
    Task<ICollection<Doctor>> GetAllDoctors();
    Task<Doctor> GetDoctByName(string name);
    Task<ICollection<DoctorBySpecialityDto>> GetDoctorsBySpeciality(string speciality);
    Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);
}
