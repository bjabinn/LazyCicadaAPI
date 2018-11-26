using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

using LazyCicada.API.Models;
using LazyCicada.API.Services;

namespace LazyCicada.API.V1.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _employeeService.Create(employee);
            return CreatedAtRoute("GetEmployeeByPk", new { pk = employee.Pk }, employee);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> Get()
            => _employeeService.Get().ToList();

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult Update([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _employeeService.Update(employee);
            return CreatedAtRoute("GetEmployeeByPk", new { pk = employee.Pk }, employee);
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _employeeService.Delete(employee);
            return NoContent();
        }
        
        [HttpGet("{id:long}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetById(long id)
        {
            var employee = _employeeService.GetByPk(id);
            if (employee == null)
            {
                employee = _employeeService.GetByEmployeeNumber(id);
                if (employee == null)
                {
                    return NotFound();
                }
            }
            return employee;
        }

        [HttpGet("{samAccountName}", Name = "GetEmployeeBySamAccountName")]
        public ActionResult<IEnumerable<Employee>> GetBySamAccountName(string samAccountName)
        {
            var employees = _employeeService.GetBySamAccountName(samAccountName).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{displayName}", Name = "GetEmployeeByDisplayName")]
        public ActionResult<IEnumerable<Employee>> GetByDisplayName(string displayName)
        {
            var employees = _employeeService.GetByDisplayName(displayName).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{name}", Name = "GetEmployeeByName")]
        public ActionResult<IEnumerable<Employee>> GetByName(string name)
        {
            var employees = _employeeService.GetByName(name).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }
    }
}