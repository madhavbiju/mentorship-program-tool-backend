using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllActiveMentorController : ControllerBase
    {
        private readonly IGetAllActiveMentorService _getAllActiveMentorService;

        public GetAllActiveMentorController(IGetAllActiveMentorService getAllActiveMentorService)
        {
            _getAllActiveMentorService = getAllActiveMentorService;
        }

        // Get all active mentors
        [HttpGet]
        public IActionResult GetAllActiveMentors()
        {
            var mentors = _getAllActiveMentorService.GetAllActiveMentors();
            return Ok(mentors);
        }
    }
}
