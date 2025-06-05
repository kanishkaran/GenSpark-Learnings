using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotifyAPI.Models;
using NotifyAPI.Models.Dtos;

namespace NotifyAPI.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> RegisterEmployee(EmployeeAddRequestDto employee);
    }
}