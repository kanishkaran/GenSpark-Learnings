using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Contexts;
using BankApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Repositories
{
    public class AccountRepository : Repository<int, Account>
    {
        public AccountRepository(BankingDbContext bankingDbContext) : base(bankingDbContext)
        {
            
        }
        public override  async Task<IEnumerable<Account>> GetAll()
        {
            var accounts = await   _context.Accounts.ToListAsync();
            return accounts.Count == 0 ? throw new Exception("Account DB Empty") : accounts;
        }

        public override async Task<Account> GetById(int id)
        {
            var account = await _context.Accounts.SingleOrDefaultAsync(ac => ac.Id == id);
            return account ?? throw new Exception("Account Not Found");
        }
    }
}