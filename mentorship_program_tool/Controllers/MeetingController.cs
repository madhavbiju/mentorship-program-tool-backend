using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.EmployeeService;
using mentorship_program_tool.Services.MeetingService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/meeting")]
    public class MeetingController : ControllerBase
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        /// <summary>
        /// To get details of all Meetings
        /// </summary>
        [HttpGet]
        public IActionResult GetMeetings()
        {
            var meeting = _meetingService.GetMeetings();
            return Ok(meeting);
        }


        /// <summary>
        /// To get details of all Meetings with pagination
        /// </summary>
        [HttpGet("{pageNumber}")]
        public IActionResult GetAllMeetings(int pageNumber, string sortBy)
        {
            var meetings = _meetingService.GetAllMeetings(pageNumber, sortBy);
            return Ok(meetings);

        }

        /// <summary>
        /// To get details of a particular Meeting
        /// </summary>
        [HttpGet("Meetings{id}")]
        public IActionResult GetMeetingById(int id)
        {
            var meeting = _meetingService.GetMeetingById(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        /// <summary>
        /// To get all meetings of a particular employee
        /// </summary>
        [HttpGet("employee/{id}")]
        public IActionResult GetMeetingByEmployeeId([FromQuery][Required] int role, int id)
        {
            var meeting = _meetingService.GetMeetingByEmployeeId(id, role);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        /// <summary>
        /// To get upcoming 3 meetings of a particular employee
        /// </summary>
        [HttpGet("employee/upcoming/meetings/{id}")]
        public IActionResult GetSoonMeetingByEmployeeId(int id)
        {
            var meeting = _meetingService.GetSoonMeetingByEmployeeId(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }

        /// <summary>
        /// To create a new Meeting
        /// </summary>
        [HttpPost]
        public IActionResult AddMeeting(MeetingSchedule meeting)
        {
            _meetingService.CreateMeeting(meeting);
            return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.MeetingID }, meeting);
        }

        /// <summary>
        /// To delete an Meeting
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            if (id != null)
            {
                _meetingService.DeleteMeeting(id);
                return NoContent();
            }

            return NotFound();
        }

        /// <summary>
        /// To get the meetings of an Employee Based on the program id.
        /// </summary>
        [HttpGet("meetings/programid/{id}")]
        public IActionResult GetMeetingsByProgramId(int id,  int page, string? sortBy)
        {
            var tasks = _meetingService.GetMeetingsByProgramId(id,  page, sortBy);
            return Ok(tasks);

        }
        /// <summary>
        /// to get meetings by employees
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        [HttpGet("meetings/employeeid/{id}")]
        public IActionResult GetMeetingsByEmployeeId(int id, int page, string? sortBy)
        {
            var tasks = _meetingService.GetMeetingsByEmployeeId(id, page, sortBy);
            return Ok(tasks);

        }
    }
}