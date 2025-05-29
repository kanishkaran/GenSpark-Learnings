using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Contexts;
using BankApi.Interfaces;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankApi.Services
{
    public class TransferAmountService : ITransferAmountService
    {
        private readonly BankingDbContext _context;

        public TransferAmountService(BankingDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionEntry>> TransferMoney(int fromAccount, int toAccount, float amount)
        {
            var fromAcc = await _context.Accounts.SingleOrDefaultAsync(ac => ac.AccountNumber == fromAccount) ?? throw new Exception($"{fromAccount} not found");
            var toAcc = await _context.Accounts.SingleOrDefaultAsync(ac => ac.AccountNumber == toAccount) ?? throw new Exception($"{toAccount} not found");

            if (fromAcc.Status == "De-Activated" || toAcc.Status == "De-Activated")
                throw new Exception("account not valid");

            var result = await makeTransaction(fromAcc, toAcc, amount);
            return result;
        }

        private async Task<IEnumerable<TransactionEntry>> makeTransaction(Account fromAcc, Account toAcc, float amount)
        {
            List<TransactionEntry> entries = new();
            using var transaction = _context.Database.BeginTransaction();

            if (fromAcc.Balance < amount)
                throw new Exception("Balance Not Enough");

            fromAcc.Balance -= amount;
            toAcc.Balance += amount;

            var fromTransaction = new TransactionEntry
            {
                AccountId = fromAcc.Id,
                AccountNumber = fromAcc.AccountNumber,
                Amount = amount,
                TransactionType = "TransferOut"
            };

            await _context.Transactions.AddAsync(fromTransaction);
            await _context.SaveChangesAsync();
            entries.Add(fromTransaction);

            var toTransaction = new TransactionEntry
            {
                AccountId = toAcc.Id,
                AccountNumber = toAcc.AccountNumber,
                Amount = amount,
                TransactionType = "TransferIn"
            };

            await _context.Transactions.AddAsync(toTransaction);
            await _context.SaveChangesAsync();
            entries.Add(toTransaction);

            await transaction.CommitAsync();
            return entries;
        }
    }
}