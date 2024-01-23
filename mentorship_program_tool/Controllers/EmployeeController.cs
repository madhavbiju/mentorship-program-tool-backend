using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employee = _employeeService.GetEmployee();
            return Ok(employee);
        }
        [HttpGet("Offset/Limit")]
        public async Task<IActionResult> GetEmployees([FromQuery] int offset, [FromQuery] int limit)
        {
            try
            {
                var employees = await _employeeService.GetEmployeesWithOffsetLimitAsync(offset, limit);
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeesById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddEmployees(EmployeeModel employee)
        {
            _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = employee.employeeid }, employee);
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id)
        {
            if (id != null)
            {
                _employeeService.DeleteEmployee(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}