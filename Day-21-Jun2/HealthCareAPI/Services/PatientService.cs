using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HealthCareAPI.Exceptions;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using HealthCareAPI.Models.DTOs;

namespace HealthCareAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IRepository<int, Patient> _patientRepository;
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<PatientService> _logger;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public PatientService(IRepository<int, Patient> patientRepository,
                              IRepository<string, User> userRepository,
                              ILogger<PatientService> logger,
                              IEncryptionService encryptionService,
                              IMapper mapper)
        {
            _patientRepository = patientRepository;
            _userRepository = userRepository;
            _logger = logger;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }
        public async Task<Patient> RegisterPatient(PatientAddRequestDto patient)
        {
            var user = _mapper.Map<PatientAddRequestDto, User>(patient);
            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = patient.Password
            });
            user.Password = encryptedData.EncryptedDate;
            user.HashKey = encryptedData.Hashkey;
            user.Role = "Patient";

            await _userRepository.Add(user);

            var newPatient = _mapper.Map<PatientAddRequestDto, Patient>(patient);
            newPatient = await _patientRepository.Add(newPatient);
            return newPatient;
        }
    }
}