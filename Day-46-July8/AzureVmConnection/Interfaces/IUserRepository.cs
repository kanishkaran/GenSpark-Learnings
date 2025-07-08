using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.models;

namespace AzureVmConnection.Interfaces
{
    public interface IUserRepository
    {
        Task<UserTest> AddUserAsync(UserTest user);
        ICollection<UserTest> GetAllUsers();
    }
}