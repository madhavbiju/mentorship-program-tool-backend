using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.AdminDashboardCountService;
using mentorship_program_tool.Services.EmployeeRoleService;
using mentorship_program_tool.Services.EmployeeService;
using mentorship_program_tool.Services.GetUserDetailsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
    [Route("api/admin")]
    [Authorize(Policy = "RequireAdminRole")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDashboardCountService _adminDashboardCountService;
        private readonly IGetUserDetailsService _getUserDetailsService;
        private readonly IEmployeeRoleService _employeeRoleService;


        public AdminController(IAdminDashboardCountService adminDashboardCountService, IGetUserDetailsService getUserDetailsService, IEmployeeRoleService employeeRoleService)
        {
            _adminDashboardCountService = adminDashboardCountService;
            _getUserDetailsService = getUserDetailsService;
            _employeeRoleService = employeeRoleService;
        }

        /// <summary>
        /// To get count of Active Mentors,Mentees and Pairs
        /// </summary>
        //getall Pending request
        [HttpGet("active-count")]
        public IActionResult GetDashboardCount()
        {
            AdminDashboardCountAPIModel admin = new AdminDashboardCountAPIModel();
            admin.MenteeCount = _adminDashboardCountService.GetAdminDashboardMenteeCount();
            admin.MentorCount = _adminDashboardCountService.GetAdminDashboardMentorCount();
            admin.ActivePairCount = _adminDashboardCountService.GetAdminDashboardProgramCount();
            admin.TotalEmployees = _adminDashboardCountService.GetAdminDashboardTotalCount();

            return Ok(admin);
        }


        /// <summary>
        /// To get details of Users based on their role
        /// </summary>
        // Get users by role with pagination
        [HttpGet("ByRole/{role}")]
        public IActionResult GetUsersByRole(string role, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return BadRequest("Role is required.");
            }

            // Validate pageNumber and pageSize
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            var users = _getUserDetailsService.GetUserDetails(role, pageNumber, pageSize);
            return Ok(users);
        }
        /// <summary>
        /// To get employee and their roles
        /// </summary>
        [HttpGet("viewrolesassigned")]
        public IActionResult GetEmployeeRoles()
        {
            var employeeRole = _employeeRoleService.GetEmployeeRoles();
            return Ok(employeeRole);
        }
        /// <summary>
        /// To get employee and their roles by id
        /// </summary>
        [HttpGet("viewrolesbyid/{id}")]
        public IActionResult GetEmployeeRoleById(int id)
        {
            var employeeRole = _employeeRoleService.GetEmployeeRoleById(id);
            if (employeeRole == null)
            {
                return NotFound();
            }
            return Ok(employeeRole);
        }
        /// <summary>
        /// To assign roles to a user
        /// </summary>
        [HttpPost("assignroletouser")]
        public IActionResult AddEmployeeRole(EmployeeRoleMapping employeeRole)
        {
            _employeeRoleService.CreateEmployeeRole(employeeRole);
            return CreatedAtAction(nameof(GetEmployeeRoleById), new { id = employeeRole.EmployeeRoleMappingID }, employeeRole);
        }
        /// <summary>
        /// To update role of a user
        /// </summary>
        [HttpPut("updateroleofuser/{id}")]
        public IActionResult UpdateEmployeeRole(int id, EmployeeRoleMapping employeeRole)
        {
            if (id != employeeRole.EmployeeID)
            {
                return BadRequest();
            }

            _employeeRoleService.UpdateEmployeeRole(id, employeeRole);
            return NoContent();
        }

    }
}


