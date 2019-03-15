using LoggerService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AccountOwnerServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public ValuesController(ILoggerManager logger)
        {
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {

            _logger.LogInfo("Here is inof message form our values controller.");
            _logger.LogDebug("Here is debug message form our values controller.");
            _logger.LogWarn("Here is warn message from out values controller.");
            _logger.LogError("Here is error message from our values controller.");

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
