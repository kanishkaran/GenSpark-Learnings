
using System.Xml.Serialization;
using AutoMapper;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Misc;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;


namespace HealthCareAPI.Services;

public class DoctorService : IDoctorService
{
    DoctorMapper doctorMapper;
    SpecializationMapper specializationMapper;

    private readonly IMapper _mapper;
    private readonly IEncryptionService _encryptionService;
    private readonly IOtherFunctionalities _otherFunctionalities;
    private readonly IRepository<int, Doctor> _doctorRepository;
    private readonly IRepository<int, Specialization> _specializationRepository;
    private readonly IRepository<int, DoctorSpecialization> _doctorSpecializationRepository;
    private readonly IRepository<string, User> _userRepository;

    public DoctorService(IRepository<int, Doctor> doctorRepository,
                        IRepository<int, Specialization> specializationRepository,
                        IRepository<int, DoctorSpecialization> doctorSpecializationRepository,
                        IOtherFunctionalities otherFunctionalities,
                        IRepository<string, User> userRepository,
                        IEncryptionService encryptionService,
                        IMapper mapper)
    {
        _doctorRepository = doctorRepository;
        _specializationRepository = specializationRepository;
        _doctorSpecializationRepository = doctorSpecializationRepository;
        _userRepository = userRepository;

        doctorMapper = new();
        specializationMapper = new();

        _otherFunctionalities = otherFunctionalities;

        _mapper = mapper;

        _encryptionService = encryptionService;
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

    public async Task<ICollection<DoctorBySpecialityDto>> GetDoctorsBySpeciality(string speciality)
    {
        try
        {
            return await _otherFunctionalities.GetDoctorsBySpeciality(speciality);
        }
        catch (Exception)
        {
            throw;
        }
    }


    public async Task<ICollection<Doctor>> GetAllDoctors()
    {
        var doctors = await _doctorRepository.GetAll();
        return [.. doctors];
    }

    public async Task<Doctor> AddDoctor(DoctorAddRequestDto doctor)
    {
        try
        {
            var user = _mapper.Map<DoctorAddRequestDto, User>(doctor);
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = doctor.Password
            });
            user.Password = encryptedData.EncryptedDate;
            user.HashKey = encryptedData.Hashkey;
            user.Role = "Doctor";
            await _userRepository.Add(user);
            
            var newDoctor = doctorMapper.MapDoctorAddRequest(doctor);
            newDoctor = await _doctorRepository.Add(newDoctor);
            if (newDoctor == null)
                throw new Exception("Doctor can't be added");

            if (doctor.Specialities.Count > 0)
            {
                int[] specialityIds = await MapAndAddSpecialization(doctor);
                for (int i = 0; i < specialityIds.Length; i++)
                {
                    var doctorSpeciality = specializationMapper.MapDoctorSpecility(newDoctor.Id, specialityIds[i]);
                    doctorSpeciality = await _doctorSpecializationRepository.Add(doctorSpeciality);
                }
            }
            return newDoctor;

        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }

    private async Task<int[]> MapAndAddSpecialization(DoctorAddRequestDto doctor)
    {
        int[] specialityIds = new int[doctor.Specialities.Count];
        IEnumerable<Specialization> allSpecilities = null;
        try
        {
            allSpecilities = await _specializationRepository.GetAll();
        }
        catch
        {

        }
        int count = 0;
        foreach (var item in doctor.Specialities)
        {
            Specialization specialization = null;
            if (allSpecilities != null)
                specialization = allSpecilities.FirstOrDefault(sp => sp.Name.ToLower() == item.Name.ToLower());
            if (specialization == null)
            {
                specialization = specializationMapper.MapSpecializationAddRequest(item);
                specialization = await _specializationRepository.Add(specialization);
            }
            specialityIds[count] = specialization.Id;
            count++;
        }

        return specialityIds;
    }
}
