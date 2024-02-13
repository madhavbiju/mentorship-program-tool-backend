﻿using mentorship_program_tool.Models.APIModel;
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
    /* [Authorize(Policy = "RequireAdminRole")]*/
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
        /*      [HttpGet("ByRole/{role}")]
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
              }*/
        /*[HttpGet]
        public ActionResult<UserDetailsResponseAPIModel> Get([FromQuery] string role = "all", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortParameter = "UserName", [FromQuery] bool isAscending = true)
        {
            if (pageSize <= 0 || pageNumber <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            try
            {
                var userDetails = _getUserDetailsService.GetUserDetails(role, pageNumber, pageSize, sortParameter, isAscending);
                if (userDetails.Users.Any())
                {
                    return Ok(userDetails);
                }
                else
                {
                    return NotFound("No users found.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
*/

        /*[HttpGet]
        public ActionResult<UserDetailsResponseAPIModel> Get([FromQuery] string role = "all", [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string sortParameter = "UserName", [FromQuery] string sortType = "Asc")
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

            try
            {
                var userDetails = _getUserDetailsService.GetUserDetails(role, pageNumber, pageSize, sortParameter, sortType);
                if (userDetails.Users.Any())
                {
                    return Ok(userDetails);
                }
                else
                {
                    return NotFound("No users found.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        [HttpGet]
        public ActionResult<UserDetailsResponseAPIModel> Get(
    [FromQuery] string role = "all",
    [FromQuery] int pageNumber = 1,
    [FromQuery] int pageSize = 10,
    [FromQuery] string sortParameter = "UserName",
    [FromQuery] string sortType = "Asc",
    [FromQuery] string status = "all") // Added status parameter
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
                var userDetails = _getUserDetailsService.GetUserDetails(role, pageNumber, pageSize, sortParameter, sortType, status); // Passed status to the service method
                if (userDetails.Users.Any())
                {
                    return Ok(userDetails);
                }
                else
                {
                    return NotFound("No users found.");
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*/// <summary>
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
        }*/
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


