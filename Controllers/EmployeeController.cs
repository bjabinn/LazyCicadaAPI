using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LazyCicadaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // GET api/emploee
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "employee_1", "employee_2" };
        }

        // GET api/emploee/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "employee_" + id;
        }

        // POST api/emploee
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/emploee/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/emploee/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
