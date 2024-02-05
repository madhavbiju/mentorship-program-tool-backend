using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.EmployeeService;
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
            var employee = _employeeService.GetEmployees();
            return Ok(employee);
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
        public IActionResult AddEmployees(Employee employee)
        {
            _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = employee.EmployeeID }, employee);
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