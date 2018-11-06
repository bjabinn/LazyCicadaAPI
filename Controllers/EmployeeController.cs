using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LazyCicadaApi.Models;
using LazyCicadaApi.Helpers;

namespace LazyCicadaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly LazyCicadaContext _context;
        public EmployeeController(LazyCicadaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Employee>> GetAll()
        {
            return _context.Employee.ToList();
        }
        
        [HttpGet("{number:long}", Name = "GetByNumber")]
        public ActionResult<Employee> GetByNumber(long number)
        {
            var employee = _context.Employee.SingleOrDefault(epy => epy.EpyNumber == number);
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }
        
        [HttpGet("{name}", Name = "GetByName")]
        public ActionResult<List<Employee>> GetByName(string name)
        {
            var employee = _context.Employee.Where(
                epy => (LazyCicadaApiHelper.RemoveAccentsWithNormalization(epy.EpyShortName.ToUpperInvariant()).Contains(LazyCicadaApiHelper.RemoveAccentsWithNormalization(name.ToUpperInvariant()))
                || LazyCicadaApiHelper.RemoveAccentsWithNormalization(String.Concat(epy.EpyFirstName," ",epy.EpyLastName).ToUpperInvariant()).Contains(LazyCicadaApiHelper.RemoveAccentsWithNormalization(name.ToUpperInvariant())))
                ).ToList();
            if (employee == null)
            {
                return NotFound();
            }
            return employee;
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            _context.Employee.Add(employee);
            _context.SaveChanges();

            return CreatedAtRoute("GetByNumber", new { number = employee.EpyNumber }, employee);
        }

        [HttpPut("{number}")]
        public IActionResult UpdateByNumber(long number, Employee employee)
        {
            var epy = _context.Employee.SingleOrDefault(e => e.EpyNumber == number);
            if (epy == null)
            {
                return NotFound();
            }

            epy.EpyNumber = employee.EpyNumber;
            epy.EpyFirstName = employee.EpyFirstName;
            epy.EpyLastName = employee.EpyLastName;
            epy.EpyShortName = employee.EpyShortName;

            _context.Employee.Update(epy);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{number}")]
        public IActionResult DeleteByNumber(long number)
        {
            var employee = _context.Employee.SingleOrDefault(epy => epy.EpyNumber == number);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
