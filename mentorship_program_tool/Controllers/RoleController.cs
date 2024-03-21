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

        /// <summary>
        /// To get all roles
        /// </summary>
        [HttpGet]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetRoles();
            return Ok(roles);
        }

        /// <summary>
        /// To get a role by id.
        /// </summary>
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

        /// <summary>
        /// To add a role
        /// </summary>
        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            _roleService.CreateRole(role);
            return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleID }, role);
        }

        /// <summary>
        /// To update a role
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult UpdateRole(int id, Role role)
        {
            if (id != role.RoleID)
            {
                return BadRequest();
            }

            _roleService.UpdateRole(id, role);
            return NoContent();
        }


        /// <summary>
        /// To delete a role.
        /// </summary>
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
