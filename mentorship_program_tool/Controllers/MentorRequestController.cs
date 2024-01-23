using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorRequestController : ControllerBase
    {
        private readonly IMentorRequestService _mentorRequestService;

        public MentorRequestController(IMentorRequestService mentorRequestService)
        {
            _mentorRequestService = mentorRequestService;
        }

        //put request
        [HttpPost]
        public IActionResult AddRequest(MentorRequestAPIModel mentorrequestapimodel)
        {
            _mentorRequestService.CreateRequest(mentorrequestapimodel);
            return Ok();
        }

        //getall Pending request
        [HttpGet("GetAll Pending Request")]
        public IActionResult GetPendingRequest()
        {
            var pendingRequest = _mentorRequestService.GetPendingRequest();
            return Ok(pendingRequest);
        }
    }
}
