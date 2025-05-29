using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;
using BankApi.Models.DTOs;

namespace BankApi.Misc
{
    public class AccountMapper
    {
        public Account AccountAddRequestMapper(AccountAddRequestDto account, int accNo)
        {
            Account newAccount = new();
            newAccount.AccountHolderName = account.AccountHolderName;
            newAccount.Balance = account.Balance;
            newAccount.Status = "Active";
            newAccount.AccountNumber = accNo;
            return newAccount;
        }

        public AccountViewResponseDto AccountViewResponseMapper(Account account)
        {
            AccountViewResponseDto newAccount = new();
            newAccount.AccountHolderName = account.AccountHolderName;
            newAccount.AccountNumber = account.AccountNumber;
            newAccount.Balance = account.Balance;
            return newAccount;
        }

        public ICollection<AccountViewResponseDto> AccountsViewResponseMapper(IEnumerable<Account> accounts)
        {
            List<AccountViewResponseDto> allAccounts = new();

            foreach (var acc in accounts)
            {
                AccountViewResponseDto newAcc = new();
                newAcc.AccountHolderName = acc.AccountHolderName;
                newAcc.AccountNumber = acc.AccountNumber;
                newAcc.Balance = acc.Balance;
                allAccounts.Add(newAcc);
            }
            return allAccounts;
        }
    }
}