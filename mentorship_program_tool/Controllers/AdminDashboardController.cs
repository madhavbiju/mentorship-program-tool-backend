using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace mentorship_program_tool.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDashboardController : ControllerBase
    {
        [Authorize("Admin")]
        [HttpGet("Admin")]
        public IActionResult Get()
        {


            // Get the user's claims
            var claimsIdentity = User.Identity as ClaimsIdentity;


            // Find the NameIdentifier claim
            var nameIdentifierClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);


            // Find the NameIdentifier claim
            var nameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            // Find the NameIdentifier claim
            var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role);



            return Ok($"Hello User, Welcome to Admin Dashboard - Name:{nameClaim.Value} - Role: Admin");
        }
        [Authorize("Mentor")]
        [HttpGet("Mentor")]
        public IActionResult GetMentor()
        {


            // Get the user's claims
            var claimsIdentity = User.Identity as ClaimsIdentity;


            // Find the NameIdentifier claim
            var nameIdentifierClaim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);


            // Find the NameIdentifier claim
            var nameClaim = claimsIdentity?.FindFirst(ClaimTypes.Name);

            // Find the NameIdentifier claim
            var roleClaim = claimsIdentity?.FindFirst(ClaimTypes.Role);



            return Ok($"Hello User, Welcome to Mentor Dashboard - Name:{nameClaim.Value} - Role: Mentor");
        }
        [Authorize("Mentee")]
            [HttpGet("Mentee")]
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



                return Ok($"Hello User, Welcome to Mentee Dashboard - Name:{nameClaim.Value} - Role: Mentee");
            }
        
            
        
    }
}
