using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace mentorship_program_tool.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MentorDashboardController : ControllerBase
    {
        [Authorize("Mentor")]
        [HttpGet]
        public IActionResult GetMentee()
        {


            // Get the user's claims
            var claimsIdentity = User.Identity as ClaimsIdentity;


            // Find the NameIdentifier claim
            var nameIdentifierClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);


            // Find the NameIdentifier claim
            var nameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            // Find the NameIdentifier claim
            var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role);



            return Ok($"Hello User, Welcome to Mentor Dashboard - Name:{nameClaim.Value} - Role:{roleClaim.Value}");
        }
    }
}
