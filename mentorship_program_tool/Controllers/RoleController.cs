using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.RoleService;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(int id)
        {
            var role = _roleService.GetRoleById(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [HttpPost]
        public IActionResult AddRole(RoleModel role)
        {
            _roleService.CreateRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.roleid }, role);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, RoleModel role)
        {
            if (id != role.roleid)
            {
                return BadRequest();
            }

            _roleService.UpdateRole(id, role);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(int id)
        {
            if (id != null)
            {
                _roleService.DeleteRole(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}
