using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Models;

namespace BankApi.Interfaces
{
    public interface ITransferAmountService
    {
        Task<IEnumerable<TransactionEntry>> TransferMoney(int fromAccount, int toAccount, float amount);
    }
}