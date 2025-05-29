using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;

namespace BankApi.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionEntry> WithdrawMoney(int accountId, float amount);
        Task<TransactionEntry> DepositMoney(int accountId, float amount);
    }
}