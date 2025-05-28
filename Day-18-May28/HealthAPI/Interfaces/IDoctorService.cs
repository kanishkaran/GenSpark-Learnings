using System;
using HealthAPI.Models;
using HealthAPI.Models.DTOs;

namespace HealthAPI.Interfaces;

public interface IDoctorService
{
    Task<ICollection<Doctor>> GetAllDoctors();
    public Task<Doctor> GetDoctByName(string name);
    public Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality);
    public Task<Doctor> AddDoctor(DoctorAddRequestDto doctor);
}
