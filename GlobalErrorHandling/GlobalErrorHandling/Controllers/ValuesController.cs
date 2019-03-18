using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace GlobalErrorHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IRepositoryWrapper _repoWrapper;
        public ValuesController(ILoggerManager logger, IRepositoryWrapper repoWrapper)
        {
            _logger = logger;
            _repoWrapper = repoWrapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var domesticAccounts = _repoWrapper.Account.FindByCondition(x => x.AccountType.Equals("Domestic"));
            var owners = _repoWrapper.Owner.FindAll();

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
