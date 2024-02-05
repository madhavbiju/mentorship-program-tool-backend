/*using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.MenteeTaskSubmissionService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenteeTaskSubmissionController : ControllerBase
    {
        private readonly IMenteeTaskSubmissionService _menteetaskSubmissionService;

        public MenteeTaskSubmissionController(IMenteeTaskSubmissionService menteetaskSubmissionService)
        {
            _menteetaskSubmissionService = menteetaskSubmissionService;
        }

        //task completion uploading my mentee, task table get updated.
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, MenteeTaskSubmissionAPIModel menteetasksubmissionapimodel)
        {
            if (id != menteetasksubmissionapimodel.TaskID)
            {
                return BadRequest();
            }

            _menteetaskSubmissionService.SubmitTask(id, menteetasksubmissionapimodel);
            return NoContent();
        }
    }
}
*/