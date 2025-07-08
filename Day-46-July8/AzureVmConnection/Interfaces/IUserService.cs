using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.DTOs;
using AzureVmConnection.models;

namespace AzureVmConnection.Interfaces
{
    public interface IUserService
    {
        ICollection<UserTest> GetAllAsync();

        Task<UserTest> AddUser(UserAddDto user);
    }
}