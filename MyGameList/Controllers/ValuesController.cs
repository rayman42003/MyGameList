using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace MyGameList.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger) {
            this._logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> GetAll() {
            _logger.LogDebug("GetAll called");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id) {
            _logger.LogDebug($"Get {id} called");
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value) {
            _logger.LogDebug($"Post {value} called");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value) {
            _logger.LogDebug($"Put id: {id} value: {value} called");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
            _logger.LogDebug($"Delete {id} called");
        }
    }
}
