using mentorship_program_tool.Services.GetUserDetailsService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    public class GetUserDetailsController : ControllerBase
    {
        private readonly IGetUserDetailsService _getUserDetailsService;

        public GetUserDetailsController(IGetUserDetailsService getUserDetailsService)
        {
            _getUserDetailsService = getUserDetailsService;
        }

        // Get users by role
        [HttpGet("ByRole/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return BadRequest("Role is required.");
            }

            var users = _getUserDetailsService.GetUserDetails(role);
            return Ok(users);
        }
    }
}
