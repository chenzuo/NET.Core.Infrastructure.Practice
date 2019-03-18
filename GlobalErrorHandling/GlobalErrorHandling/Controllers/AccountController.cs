using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobalErrorHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public AccountController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllAccounts()
        {
            try
            {
                var accounts = _repository.Account.GetAllAccounts();

                _logger.LogInfo($"Returned all accounts from database.");

                return Ok(accounts);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllAccounts action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody]Account account)
        {
            try
            {
                if (account == null)
                {
                    _logger.LogError("Object sent from client is null.");
                    return BadRequest("Object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid object sent from client.");
                    return BadRequest("Invalid model object");
                }

                _repository.Account.CreateAccount(account);

                return CreatedAtRoute("AccountById", new { id = account.AccountId }, account);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateAccount action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}