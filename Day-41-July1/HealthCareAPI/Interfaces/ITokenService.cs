using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthCareAPI.Models;

namespace HealthCareAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}