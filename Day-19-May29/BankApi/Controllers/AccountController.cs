using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankApi.Interfaces;
using BankApi.Models;
using BankApi.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<ActionResult<Account>> CreateAccount([FromBody] AccountAddRequestDto account)
        {
            try
            {
                var result = await _accountService.CreateAccount(account);
                if (result != null)
                    return Created("", result);
                return BadRequest("Account Cant be Created Now");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpGet("Id")]
        public async Task<ActionResult<AccountViewResponseDto>> GetAccountById(int id)
        {
            try
            {
                var account = await _accountService.ViewAccount(id);
                if (account != null)
                {
                    return account;
                }
                return BadRequest("Account cannot be viewed Right Now");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountViewResponseDto>>> ViewAllAccounts()
        {
            try
            {
                var result = await _accountService.ViewAllAccounts();
                if (result == null)
                {
                    return BadRequest("Accounts Cannot Be Retrieved");
                }
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<Account>> DeleteAccount(int id)
        {
            try
            {
                var result = await _accountService.DeleteAccount(id);
                return result == null ? BadRequest("Not Able to Delete") : Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}