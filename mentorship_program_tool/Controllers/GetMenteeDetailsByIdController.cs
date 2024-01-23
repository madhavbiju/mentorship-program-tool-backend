using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetMenteeDetailsByIdController : ControllerBase

    {
        private readonly IGetMenteeDetailsByIdService _getMenteeDetailsByIdService;

        public GetMenteeDetailsByIdController(IGetMenteeDetailsByIdService GetMenteeDetailsByIdService)
        {
            _getMenteeDetailsByIdService = GetMenteeDetailsByIdService;
        }

        [HttpGet("{id}")]
        public IActionResult GetDetailsById(int id)
        {
            var details = _getMenteeDetailsByIdService.GetDetailsById(id);
            if (details == null)
            {
                return NotFound();
            }
            return Ok(details);
        }
    }
}
