using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.AdminDashboardCountService;
using mentorship_program_tool.Services.GetUserDetailsService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminDashboardCountService _adminDashboardCountService;
        private readonly IGetUserDetailsService _getUserDetailsService;

        public AdminController(IAdminDashboardCountService adminDashboardCountService, IGetUserDetailsService getUserDetailsService)
        {
            _adminDashboardCountService = adminDashboardCountService;
            _getUserDetailsService = getUserDetailsService;
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

    }
}


