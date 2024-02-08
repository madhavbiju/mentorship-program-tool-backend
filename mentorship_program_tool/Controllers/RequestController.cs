using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.AdminApprovalRequestService;
using mentorship_program_tool.Services.MentorRequestService;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/request")]
    public class RequestController : ControllerBase
    {
        private readonly IAdminApprovalRequestService _adminapprovalrequestService;
        private readonly IMentorRequestService _mentorRequestService;

        public RequestController(IAdminApprovalRequestService adminapprovalrequestService, IMentorRequestService mentorRequestService)
        {
            _adminapprovalrequestService = adminapprovalrequestService;
            _mentorRequestService = mentorRequestService;
        }

        /// <summary>
        /// To approve mentor's program extension request.
        /// </summary>
        //approving program extension request from mentor
        [HttpPut("approve{id}")]
        public IActionResult UpdateRequest(int id, AdminApprovalAPIModel adminapprovalapimodel)
        {
            if (id != adminapprovalapimodel.ProgramExtensionID)
            {
                return BadRequest();
            }

            _adminapprovalrequestService.UpdateRequest(id, adminapprovalapimodel);
            return NoContent();
        }


        /// <summary>
        /// To post a program duration extension request by mentor
        /// </summary>
        //put request
        [HttpPost("create/program-extension")]
        public IActionResult AddRequest(MentorRequestAPIModel mentorrequestapimodel)
        {
            _mentorRequestService.CreateRequest(mentorrequestapimodel);
            return Ok();
        }

        /// <summary>
        /// To get all pending request
        /// </summary>
        //getall Pending request
        [HttpGet("all")]
        public IActionResult GetPendingRequest([FromQuery][Required] int status, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            // Validate pageNumber and pageSize
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }
            var pendingRequest = _mentorRequestService.GetPendingRequests(status, pageNumber, pageSize);
            return Ok(pendingRequest);
        }


    }
}
