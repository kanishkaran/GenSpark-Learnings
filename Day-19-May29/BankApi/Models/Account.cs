using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        public string AccountHolderName { get; set; } = string.Empty;
        public float Balance { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TransactionEntry>? Transactions { get; set; }
    }
}