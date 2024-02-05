﻿using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.AdminApprovalRequestService;
using mentorship_program_tool.Services.MentorRequestService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/ProgramExtensionRequest")]
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
        [HttpPut("{id}")]
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
        [HttpPost]
        public IActionResult AddRequest(MentorRequestAPIModel mentorrequestapimodel)
        {
            _mentorRequestService.CreateRequest(mentorrequestapimodel);
            return Ok();
        }

        /// <summary>
        /// To get all pending request
        /// </summary>
        //getall Pending request
        [HttpGet("GetAll Pending Request")]
        public IActionResult GetPendingRequest()
        {
            var pendingRequest = _mentorRequestService.GetPendingRequests();
            return Ok(pendingRequest);
        }


    }
}