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

        [HttpGet("byrole")]
        public ActionResult<UserDetailsResponseAPIModel> Get(
        [FromQuery] string role = "all",
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string sortParameter = "UserName",
        [FromQuery] string sortType = "Asc",
        [FromQuery] string status = "all", // Existing status parameter
        [FromQuery] string searchQuery = "") // Added searchQuery parameter
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            // Validate sortType
            if (!sortType.Equals("Asc", StringComparison.OrdinalIgnoreCase) && !sortType.Equals("Desc", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("SortType must be either 'Asc' or 'Desc'.");
            }

            // Optional: Validate status
            if (!status.Equals("all", StringComparison.OrdinalIgnoreCase) &&
                !status.Equals("active", StringComparison.OrdinalIgnoreCase) &&
                !status.Equals("inactive", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Status must be either 'all', 'active', or 'inactive'.");
            }

            try
            {
                // Passed searchQuery to the service method along with other parameters
                var userDetails = _getUserDetailsService.GetUserDetails(role, pageNumber, pageSize, sortParameter, sortType, status, searchQuery);
                if (userDetails.Users.Any())
                {
                    return Ok(userDetails);
                }
                else
                {
                    return Ok(userDetails);
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// To assign mentor or mentee roles to user by admin
        /// </summary>
        [HttpPost("assignroles")]
        public IActionResult AssignRoles([FromBody] AssignRolesToEmployeeModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Assuming the adminUserId comes from the authenticated user's claims or context
                int adminUserId = GetAdminUserIdFromClaims();

                _employeeRoleService.UpdateEmployeeRoles(model, adminUserId);

                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception details
                // Consider logging the error for debugging purposes
                return StatusCode(500, "An error occurred while assigning roles.");
            }
        }

        private int GetAdminUserIdFromClaims()
        {
            // Placeholder for fetching the admin's user ID from the claims
            // This needs to be replaced with actual code to fetch the authenticated user's ID
            return 1; // Example user ID
        }

    }
}


