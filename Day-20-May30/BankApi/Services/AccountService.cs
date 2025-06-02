using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Interfaces;
using BankApi.Misc;
using BankApi.Models;
using BankApi.Models.DTOs;

namespace BankApi.Services
{
    public class AccountService : IAccountService
    {
        AccountMapper accountMapper;
        private readonly IRepository<int, Account> _accountRepository;
        public AccountService(IRepository<int, Account> accountRepository)
        {
            _accountRepository = accountRepository;
            accountMapper = new();
            
        }
        public async Task<Account> CreateAccount(AccountAddRequestDto account)
        {
            try
            {
                var accountNumber = GenerateAccountNumber();
                var newAccount = accountMapper.AccountAddRequestMapper(account, accountNumber);
                newAccount = await _accountRepository.Add(newAccount);
                return newAccount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private int GenerateAccountNumber()
        {
            return new Random().Next(10000000, 99999999);
        }

        public async Task<AccountViewResponseDto> ViewAccount(int id)
        {
            try
            {
                var result = await _accountRepository.GetById(id);
                var newAcc = accountMapper.AccountViewResponseMapper(result);
                return newAcc;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<AccountViewResponseDto>> ViewAllAccounts()
        {
            try
            {
                var result = await _accountRepository.GetAll();
                var accounts = accountMapper.AccountsViewResponseMapper(result);
                return accounts;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> DeleteAccount(int accountNumber)
        {
            try
            {
                var accounts = await _accountRepository.GetAll();
                var accountToDelete = accounts.FirstOrDefault(ac => ac.AccountNumber == accountNumber);
                if (accountToDelete == null)
                    throw new Exception("Account Not Found");
                var deletedAccount = ChangeStatus(accountToDelete);
                deletedAccount = await _accountRepository.Update(deletedAccount.Id, deletedAccount);
                return deletedAccount;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Account ChangeStatus(Account accountToDelete)
        {
            accountToDelete.Status = "De-Activated";
            return accountToDelete;
        }
    }
}