using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;
using BankApi.Models.DTOs;

namespace BankApi.Interfaces
{
    public interface IAccountService
    {
        Task<Account> CreateAccount(AccountAddRequestDto account);

        Task<AccountViewResponseDto> ViewAccount(int id);

        Task<ICollection<AccountViewResponseDto>> ViewAllAccounts();

        Task<Account> DeleteAccount(int accountNumber);

    }
}