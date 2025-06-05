using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyAPI.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; } = string.Empty;
        public byte[]? Password { get; set; }
        public byte[]? HashKey { get; set; }
        public string Role { get; set; } = string.Empty;
        public Employee? Employee { get; set; }
    }
}