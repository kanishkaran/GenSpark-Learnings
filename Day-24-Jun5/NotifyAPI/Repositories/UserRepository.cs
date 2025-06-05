using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotifyAPI.Contexts;
using NotifyAPI.Models;

namespace NotifyAPI.Repositories
{
public class UserRepository : Repository<string, User>
    {
        public UserRepository(NotifyDBContext context) : base(context)
        {

        }

        public override async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users.Count == 0 ? throw new Exception("No users found") : users;
        }

        public override async Task<User> GetById(string id)
        {
            var result = await _context.Users.SingleOrDefaultAsync(u => u.UserName == id);
            
            return result ?? throw new Exception("User not found");
        }
    }
}