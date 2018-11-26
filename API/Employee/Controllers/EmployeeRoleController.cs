using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LazyCicada.API.Models;
using LazyCicada.API.Services;

namespace LazyCicada.API.Controllers
{
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeRoleController : ControllerBase
    {
        private readonly IEmployeeDTOService _employeeDTOService;
        // private readonly IRoleService _roleService;
        public EmployeeRoleController(IEmployeeDTOService employeeDTOService)
            => _employeeDTOService = employeeDTOService;

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeDTO>> Get()
            => _employeeDTOService.Get().ToList();
        
        [HttpGet("{id:long}", Name = "GetEmployeeDTOById")]
        public ActionResult<EmployeeDTO> GetById(long id)
        {
            var employee = _employeeDTOService.GetByPk(id);
            if (employee == null)
            {
                employee = _employeeDTOService.GetByEmployeeNumber(id);
                if (employee == null)
                {
                    return NotFound();
                }
            }
            return employee;
        }

        [HttpGet("{samAccountName}", Name = "GetEmployeeDTOBySamAccountName")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetBySamAccountName(string samAccountName)
        {
            var employees = _employeeDTOService.GetBySamAccountName(samAccountName).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{displayName}", Name = "GetEmployeeDTOByDisplayName")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetByDisplayName(string displayName)
        {
            var employees = _employeeDTOService.GetByDisplayName(displayName).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{name}", Name = "GetEmployeeDTOByName")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetByName(string name)
        {
            var employees = _employeeDTOService.GetByName(name).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }

        [HttpGet("{roleName}", Name = "GetEmployeeDTOByRoleName")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetByRoleName(string roleName)
        {
            var employees = _employeeDTOService.GetByRoleName(roleName).ToList();
            if (employees == null)
            {
                return NotFound();
            }
            return employees;
        }
    }
}
