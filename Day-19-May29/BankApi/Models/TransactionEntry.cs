using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Models
{
    public class TransactionEntry
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int AccountNumber { get; set; }
        public string TransactionType { get; set; } = string.Empty;
        public float Amount { get; set; }

        public Account? Account { get; set; }
    }
}