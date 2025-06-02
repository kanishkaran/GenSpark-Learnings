using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models.DTOs
{
    public class AccountViewResponseDto
    {
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public float Balance { get; set; }
    }
}