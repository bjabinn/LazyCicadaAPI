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
    public class RoleController : ControllerBase
    {
        // private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService) /*IEmployeeService employeeService, */
        {
            // _employeeService = employeeService;
            _roleService = roleService;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult Create([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Create(role);
            return CreatedAtRoute("GetRoleByPk", new { pk = role.Pk }, role);
        }


        [HttpGet]
        public ActionResult<IEnumerable<Role>> Get()
            => _roleService.Get().ToList();

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult Update([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Update(role);
            return CreatedAtRoute("GetRoleByPk", new { pk = role.Pk }, role);
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(Role))]
        [ProducesResponseType(400)]
        public IActionResult Delete([FromBody] Role role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _roleService.Delete(role);
            return NoContent();
        }
        
        [HttpGet("{pk:long}", Name = "GetRoleByPk")]
        public ActionResult<Role> GetByPk(long pk)
        {
            var role = _roleService.GetByPk(pk);
            if (role == null)
            {
                return NotFound();
            }
            return role;
        }

        [HttpGet("{name}", Name = "GetRoleByName")]
        public ActionResult<IEnumerable<Role>> GetByName(string name)
        {
            var roles = _roleService.GetByName(name).ToList();
            if (roles == null)
            {
                return NotFound();
            }
            return roles;
        }
    }
}
