using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using HealthCareAPI.Contexts;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Repositories
{
    public class UserRepository : Repository<string, User>
    {
        public UserRepository(HealthCareDbContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users.Count == 0 ? throw new Exception("No users found") : users;
        }

        public override async Task<User> GetById(string id)
        {
            var result = await _context.Users.SingleOrDefaultAsync(u => u.Username == id);
            
            return result ?? throw new Exception("User not found");
        }
    }
}