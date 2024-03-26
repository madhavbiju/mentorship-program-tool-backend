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
        private readonly ILogger _logger;

        public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        /// <summary>
        /// To get details of all Employees
        /// </summary>
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                /*throw new Exception("Simulated exception in GetEmployees method");*/
                var employee = _employeeService.GetEmployees();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while getting employees.Exception:{ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// To get details of a particular Employee
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult GetEmployeesById(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while getting employee with ID {id}. Exception: {ex.Message}");
                return NotFound();
            }
        }

        /// <summary>
        /// To create a new Employee
        /// </summary>
        [HttpPost]
        public IActionResult AddEmployees(Employee employee)
        {
            try
            {
                _employeeService.CreateEmployee(employee);
                return CreatedAtAction(nameof(GetEmployeesById), new { id = employee.EmployeeID }, employee);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while adding employee. Exception: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// To delete an Employee
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployees(int id)
        {
            try
            {
                if (id != null)
                {
                    _employeeService.DeleteEmployee(id);
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while deleting employee with ID {id}. Exception: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}