using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeRoleMapController : ControllerBase
    {
        private readonly IEmployeeRoleMapService _employeeRoleMapService;

        public EmployeeRoleMapController(IEmployeeRoleMapService employeeRoleMapService)
        {
            _employeeRoleMapService = employeeRoleMapService;
        }
        [HttpPost]
        public IActionResult AddEmployees(EmployeeRoleMappingAPI employee)
        {
            _employeeRoleMapService.CreateEmployeeRoleMap(employee);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, EmployeeRoleMappingAPI employee)
        {
            if (id != employee.employeerolemappingid)
            {
                return BadRequest();
            }

            _employeeRoleMapService.UpdateEmployeeRoleMap(id, employee);
            return NoContent();
        }

    }
}
