using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.AdminApprovalRequestService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminApprovalRequestController : ControllerBase
    {
        private readonly IAdminApprovalRequestService _adminapprovalrequestService;

        public AdminApprovalRequestController(IAdminApprovalRequestService adminapprovalrequestService)
        {
            _adminapprovalrequestService = adminapprovalrequestService;
        }

        //approving program extension request from mentor
        [HttpPut("{id}")]
        public IActionResult UpdateRequest(int id, AdminApprovalAPIModel adminapprovalapimodel)
        {
            if (id != adminapprovalapimodel.programextensionid)
            {
                return BadRequest();
            }

            _adminapprovalrequestService.UpdateRequest(id, adminapprovalapimodel);
            return NoContent();
        }
    }
}
