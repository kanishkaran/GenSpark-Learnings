using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Interfaces;
using BankApi.Models;

namespace BankApi.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<int, TransactionEntry> _transactionRepository;
        private readonly IRepository<int, Account> _accountRepository;

        public TransactionService(IRepository<int, TransactionEntry> transactionRepository,
                                  IRepository<int, Account> accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }
        public async Task<TransactionEntry> DepositMoney(int accountNumber, float amount)
        {
            try
            {
                var allAccounts = await _accountRepository.GetAll();
                var acc = allAccounts.FirstOrDefault(ac => ac.AccountNumber == accountNumber);
                if (acc == null || acc.Status == "De-Activated")
                    throw new Exception("Account is not Valid");
                acc.Balance += amount;

                var transaction = new TransactionEntry
                {
                    AccountId = acc.Id,
                    AccountNumber = acc.AccountNumber,
                    Amount = amount,
                    TransactionType = "Deposit"
                };

                return await _transactionRepository.Add(transaction);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TransactionEntry> WithdrawMoney(int accountNumber, float amount)
        {
            try
            {
                var allAccounts = await _accountRepository.GetAll();
                var acc = allAccounts.FirstOrDefault(ac => ac.AccountNumber == accountNumber);

                if (acc == null || acc.Status == "De-Activated")
                    throw new Exception("Account is not Valid");
                if (acc.Balance < amount)
                    throw new Exception("Not Enough Balance");
                acc.Balance -= amount;

                var transaction = new TransactionEntry
                {
                    AccountId = acc.Id,
                    AccountNumber = acc.AccountNumber,
                    Amount = amount,
                    TransactionType = "Withdraw"
                };

                return await _transactionRepository.Add(transaction);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}