using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.DTOs;
using AzureVmConnection.Interfaces;
using AzureVmConnection.models;

namespace AzureVmConnection.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserTest> AddUser(UserAddDto user)
        {
            try
            {
                var newUser = new UserTest
                {
                    Name = user.Name,
                    Age = user.Age
                };
                var addedUser = await _userRepository.AddUserAsync(newUser);
                return addedUser;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public ICollection<UserTest> GetAllAsync()
        {
            try
            {
                return  _userRepository.GetAllUsers();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }
    }
}