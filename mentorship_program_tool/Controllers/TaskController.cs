using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetTasksbyEmployeeIdService;
using mentorship_program_tool.Services.MenteeTaskSubmissionService;
using mentorship_program_tool.Services.MentorTaskRepository;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly IMenteeTaskSubmissionService _menteetaskSubmissionService;
        private readonly IMentorTaskService _mentorTaskService;
        private readonly IGetTasksbyEmployeeIdService _getTasksByEmployeeIdService;
        private readonly IGetTasksByProgramIdService _getTasksByProgramIdService;



        public TaskController(IMenteeTaskSubmissionService menteetaskSubmissionService, IMentorTaskService mentorTaskService, IGetTasksbyEmployeeIdService GetTasksByEmployeeIdService, IGetTasksByProgramIdService GetTasksByProgramIdService)
        {
            _menteetaskSubmissionService = menteetaskSubmissionService;
            _mentorTaskService = mentorTaskService;
            _getTasksByEmployeeIdService = GetTasksByEmployeeIdService;
            _getTasksByProgramIdService = GetTasksByProgramIdService;


        }


        /// <summary>
        /// To update Task status to submitted
        /// </summary>
        //task completion uploading my mentee, task table get updated.
        [HttpPut("submit/{id}")]
        public IActionResult UpdateTask(int id, MenteeTaskSubmissionAPIModel menteetasksubmissionapimodel)
        {
            if (id != menteetasksubmissionapimodel.taskid)
            {
                return BadRequest();
            }

            _menteetaskSubmissionService.SubmitTask(id, menteetasksubmissionapimodel);
            return NoContent();
        }


        /// <summary>
        /// To post a task
        /// </summary>
        //put task
        [HttpPost("/create")]
        public IActionResult AddTask(MentorTaskAPIModel mentortaskapimodel)
        {
            _mentorTaskService.CreateTask(mentortaskapimodel);
            return Ok();
        }


        /// <summary>
        /// To update Task status to appproved.
        /// </summary>
        //update status of task completed by mentee
        [HttpPut("/mark-as-done/{id}")]
        public IActionResult UpdateStatus(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel)
        {
            if (id != taskstatusupdationmodel.TaskID)
            {
                return BadRequest();
            }

            _mentorTaskService.UpdateStatusOfTask(id, taskstatusupdationmodel);
            return NoContent();
        }


        /// <summary>
        /// To Updating due date of task.
        /// </summary>
        //update due date of task 
        [HttpPut("/modify/{id}")]
        public IActionResult UpdateDueDate(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel)
        {
            if (id != taskenddateupdationmodel.TaskID)
            {
                return BadRequest();
            }

            _mentorTaskService.UpdateEndDateOfTask(id, taskenddateupdationmodel);
            return NoContent();
        }


        /// <summary>
        /// To get the tasks of an Employee Based on the mentor id and status.
        /// </summary>
        [HttpGet("Mentor/{id},{status}")]
        public IActionResult GetTasksByEmployeeId(int id, int status, int page)
        {
            var tasks = _getTasksByEmployeeIdService.GetTasksByEmployeeId(id, status, page);
            return Ok(tasks);

        }

        /// <summary>
        /// To get the tasks of an Employee Based on the program id and status.
        /// </summary>
        [HttpGet("Program/{id},{status}")]
        public IActionResult GetTasksByProgramId(int id, int status, int page)
        {
            var tasks = _getTasksByProgramIdService.GetTasksByProgramId(id, status, page);
            return Ok(tasks);

        }


    }
}
