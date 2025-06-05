using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NotifyAPI.Models;

namespace NotifyAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateToken(User user);
    }
}