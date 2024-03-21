using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.EmployeeService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// To get details of all Employees
        /// </summary>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employee = _employeeService.GetEmployees();
            return Ok(employee);
        }

        /// <summary>
        /// To get details of a particular Employee
        /// </summary>
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

        /// <summary>
        /// To create a new Employee
        /// </summary>
        [HttpPost]
        public IActionResult AddEmployees(Employee employee)
        {
            _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetEmployeesById), new { id = employee.EmployeeID }, employee);
        }

        /// <summary>
        /// To delete an Employee
        /// </summary>
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