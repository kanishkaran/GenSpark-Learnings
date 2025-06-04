using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCareAPI.Models.DTOs
{
    public class ErrorObjectDto
    {
        public int ErrorNumber { get; set; }

        public string ErrorName { get; set; } = string.Empty;
    }
}