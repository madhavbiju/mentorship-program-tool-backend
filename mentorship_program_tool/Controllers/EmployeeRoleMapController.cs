using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.EmployeeRoleService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRoleMapController : ControllerBase
    {
        private readonly IEmployeeRoleService _employeeRoleService;

        public EmployeeRoleMapController(IEmployeeRoleService employeeRoleService)
        {
            _employeeRoleService = employeeRoleService;
        }

        [HttpGet]
        public IActionResult GetEmployeeRoles()
        {
            var employeeRole = _employeeRoleService.GetEmployeeRoles();
            return Ok(employeeRole);
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeRoleById(int id)
        {
            var employeeRole = _employeeRoleService.GetEmployeeRoleById(id);
            if (employeeRole == null)
            {
                return NotFound();
            }
            return Ok(employeeRole);
        }

        [HttpPost]
        public IActionResult AddEmployeeRole(EmployeeRoleMappingModel employeeRole)
        {
            _employeeRoleService.CreateEmployeeRole(employeeRole);
            return CreatedAtAction(nameof(GetEmployeeRoleById), new { id = employeeRole.EmployeeRoleMappingId }, employeeRole);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEmployeeRole(int id, EmployeeRoleMappingModel employeeRole)
        {
            if (id != employeeRole.EmployeeId)
            {
                return BadRequest();
            }

            _employeeRoleService.UpdateEmployeeRole(id, employeeRole);
            return NoContent();
        }
    }
}
