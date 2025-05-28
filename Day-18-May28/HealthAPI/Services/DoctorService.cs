using System;
using HealthAPI.Interfaces;
using HealthAPI.Models;
using HealthAPI.Models.DTOs;
using HealthAPI.Repositories;

namespace HealthAPI.Services;

public class DoctorService : IDoctorService
{

    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Specialization> _specializationRepository;
    private readonly IRepository<int, DoctorSpecialization> _doctorSpecializationRepository;
    public DoctorService(IRepository<int, Doctor> doctorRepository,
                        IRepository<int, Specialization> specializationRepository,
                        IRepository<int, DoctorSpecialization> doctorSpecializationRepository)
    {
        _doctorRepository = doctorRepository;
        _specializationRepository = specializationRepository;
        _doctorSpecializationRepository = doctorSpecializationRepository;
    }
    public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctor)
    {
        try
        {
            var doc = new Doctor
            {
                DoctorName = doctor.Name,
                Status = "Active"
            };
            await _doctorRepository.Add(doc);

            if (doctor.Specialities != null)
            {
                var everySpecialization = await _specializationRepository.GetAll() ?? new List<Specialization>();
                foreach (var specialization in doctor.Specialities)
                {
                    var addedSpecialization = everySpecialization.FirstOrDefault(sp => sp.Name == specialization.Name);

                    if (addedSpecialization == null)
                    {
                        var spec = new Specialization
                        {
                            Name = specialization.Name,
                            Status = "Active"
                        };

                        await _specializationRepository.Add(spec);

                        var docSpec = new DoctorSpecialization
                        {
                            SpecializationId = spec.Id,
                            DoctorId = doc.Id
                        };

                        await _doctorSpecializationRepository.Add(docSpec);

                    }
                    else
                    {

                        var docSpecialization = new DoctorSpecialization
                        {
                            SpecializationId = addedSpecialization.Id,
                            DoctorId = doc.Id
                        };

                        await _doctorSpecializationRepository.Add(docSpecialization);
                    }
                }
            }

            return doc;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Doctor();
        }
    }

    public async Task<Doctor> GetDoctByName(string name)
    {
        try
        {
            var doctors = await _doctorRepository.GetAll();
            var doctor = doctors.FirstOrDefault(doc => doc.DoctorName.ToLower() == name);
            if (doctor != null)
                return doctor;
            return new Doctor();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new Doctor();
        }
    }

    public async Task<ICollection<Doctor>> GetDoctorsBySpeciality(string speciality)
    {
        try
        {
            var everySpecialization = await _specializationRepository.GetAll();
            var specialization = everySpecialization.FirstOrDefault(sp => sp.Name == speciality);
            if (specialization == null)
                return new List<Doctor>();

            var doctorSpecs = await _doctorSpecializationRepository.GetAll();


            var doctorsInSpeciality = doctorSpecs.Where(sp => sp.SpecializationId == specialization.Id)
                                .Select(doc => doc.DoctorId);


            var everyDoctor = await _doctorRepository.GetAll();

            var result = everyDoctor.Where(doc => doctorsInSpeciality.Contains(doc.Id)).ToList();
            return result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<Doctor>();
        }
    }


    public async Task<ICollection<Doctor>> GetAllDoctors()
    {
        var doctors = await _doctorRepository.GetAll();
        return [.. doctors];
    }
}
