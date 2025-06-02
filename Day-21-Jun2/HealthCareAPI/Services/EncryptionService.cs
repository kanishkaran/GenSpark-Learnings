using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HealthCareAPI.Interfaces;
using HealthCareAPI.Models;

namespace HealthCareAPI.Services
{
    public class EncryptionService : IEncryptionService
    {
        public async Task<EncryptModel> EncryptData(EncryptModel data)
        {
            HMACSHA256 hMACSHA256;
            if (data.Hashkey != null)
            {
                hMACSHA256 = new HMACSHA256(data.Hashkey);
            }
            else
            {
                hMACSHA256 = new HMACSHA256();
            }

            data.EncryptedDate = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(data.Data));
            data.Hashkey = hMACSHA256.Key;
            return data;
        }
    }
}