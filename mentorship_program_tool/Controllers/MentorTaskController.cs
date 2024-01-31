using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.MentorTaskRepository;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MentorTaskController : ControllerBase
    {
        private readonly IMentorTaskService _mentorTaskService;

        public MentorTaskController(IMentorTaskService mentorTaskService)
        {
            _mentorTaskService = mentorTaskService;
        }
        //put task
        [HttpPost]
        public IActionResult AddTask(MentorTaskAPIModel mentortaskapimodel)
        {
            _mentorTaskService.CreateTask(mentortaskapimodel);
            return Ok();
        }

        //update status of task completed by mentee
        [HttpPut("{id} Updating Completed task Status")]
        public IActionResult UpdateStatus(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel)
        {
            if (id != taskstatusupdationmodel.TaskID)
            {
                return BadRequest();
            }

            _mentorTaskService.UpdateStatusOfTask(id, taskstatusupdationmodel);
            return NoContent();
        }

        //update due date of task 
        [HttpPut("{id} Updating due date of task")]
        public IActionResult UpdateDueDate(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel)
        {
            if (id != taskenddateupdationmodel.TaskID)
            {
                return BadRequest();
            }

            _mentorTaskService.UpdateEndDateOfTask(id, taskenddateupdationmodel);
            return NoContent();
        }

    }
}
