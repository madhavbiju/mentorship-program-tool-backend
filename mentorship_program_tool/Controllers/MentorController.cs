using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/mentor")]
    public class MentorController : ControllerBase
    {
        private readonly IGetAllActiveMentorService _getAllActiveMentorService;

        public MentorController(IGetAllActiveMentorService getAllActiveMentorService)
        {
            _getAllActiveMentorService = getAllActiveMentorService;
        }

        // Get all active mentors
        [HttpGet("active")]
        public IActionResult GetAllActiveMentors()
        {
            var mentors = _getAllActiveMentorService.GetAllActiveMentors();
            return Ok(mentors);
        }
    }
}
