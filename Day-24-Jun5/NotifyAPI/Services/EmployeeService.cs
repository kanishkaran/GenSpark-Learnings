using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NotifyAPI.Interfaces;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Services
{

    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<int, Employee> _employeeRepo;
        private readonly IRepository<string, User> _userRepo;
        private readonly IEncryptionService _encryptionService;
        private readonly IMapper _mapper;

        public EmployeeService(IRepository<int, Employee> employeeRepo,
                               IRepository<string, User> userRepo,
                               IEncryptionService encryptionService,
                               IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _userRepo = userRepo;
            _encryptionService = encryptionService;
            _mapper = mapper;
        }
        public async Task<Employee> RegisterEmployee(EmployeeAddRequestDto employee)
        {
            var user = _mapper.Map<EmployeeAddRequestDto, User>(employee);

            var encryptedData = await _encryptionService.EncryptData(new EncryptModel
            {
                Data = employee.Password
            });

            user.Password = encryptedData.EncryptedDate;
            user.HashKey = encryptedData.Hashkey;

            await _userRepo.Add(user);

            var emp = _mapper.Map<EmployeeAddRequestDto, Employee>(employee);

            emp = await _employeeRepo.Add(emp);

            return emp;
        }
    }
}