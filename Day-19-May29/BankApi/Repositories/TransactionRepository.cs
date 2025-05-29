using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Contexts;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Repositories
{
    public class TransactionRepository : Repository<int, TransactionEntry>
    {

        public TransactionRepository(BankingDbContext context) : base(context)
        {
            
        }
        public override async Task<IEnumerable<TransactionEntry>> GetAll()
        {
            var transactions = await  _context.Transactions.ToListAsync();
            return transactions.Count == 0 ? throw new Exception("Transactions is Empty") : transactions;
        }

        public override async Task<TransactionEntry> GetById(int id)
        {
            var transaction = await _context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
            return transaction ?? throw new Exception("Transaction Not Found");
        }
    }
}