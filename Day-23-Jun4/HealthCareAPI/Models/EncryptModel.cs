using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Models
{
    public class EncryptModel
    {
        public string? Data { get; set; }
        public byte[]? EncryptedDate { get; set; }
        public byte[]? Hashkey { get; set; }
    }
}