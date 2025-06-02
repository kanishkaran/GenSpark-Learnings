using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Interfaces;
using BankApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransferAmountService _tranferAmountService;

        public TransactionController(ITransactionService transactionService,
                                    ITransferAmountService transferAmountService)
        {
            _transactionService = transactionService;
            _tranferAmountService = transferAmountService;
        }


        [HttpPost("Deposit")]
        public async Task<ActionResult<TransactionEntry>> DepositAmount(int accountNumber, float amount)
        {
            try
            {
                var result = await _transactionService.DepositMoney(accountNumber, amount);
                if (result != null)
                    return Ok(result);
                return BadRequest("Cant be Deposited");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Withdraw")]
        public async Task<ActionResult<TransactionEntry>> WithdrawAmount(int accountNumber, float amount)
        {
            try
            {
                var result = await _transactionService.WithdrawMoney(accountNumber, amount);
                if (result != null)
                    return Ok(result);
                return BadRequest("Cant Withdraw money right now");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("TransferMoney")]
        public async Task<ActionResult<IEnumerable<TransactionEntry>>> TranferMoney(int fromAcc, int toAcc, float amount)
        {
            try
            {
                var result = await _tranferAmountService.TransferMoney(fromAcc, toAcc, amount);
                return result == null ? throw new Exception("Not Transfered") : Ok(result);
            }
            catch (Exception e)
            {
               return BadRequest(e.Message);
            }
        }
    }
}