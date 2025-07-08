using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureVmConnection.Context;
using AzureVmConnection.Interfaces;
using AzureVmConnection.models;

namespace AzureVmConnection.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AzureDbContext _context;

        public UserRepository(AzureDbContext context)
        {
            _context = context;
        }

        public async Task<UserTest> AddUserAsync(UserTest user)
        {
          
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public ICollection<UserTest> GetAllUsers()
        {
            var users = _context.UserTests.ToList() ?? throw new Exception("No user found");

            return users;
        }
    }
}