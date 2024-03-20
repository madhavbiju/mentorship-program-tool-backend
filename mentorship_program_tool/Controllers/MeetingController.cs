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
        private readonly ILogger _logger;

        public MeetingController(IMeetingService meetingService, ILogger<MeetingController> logger)
        {
            _meetingService = meetingService;
            _logger = logger;
        }

        /// <summary>
        /// To get details of all Meetings
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMeetings()
        {
            try
            {
                var meeting = await _meetingService.GetMeetings();
                return Ok(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meetings. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// To get details of all Meetings with pagination
        /// </summary>
        [HttpGet("{pageNumber}")]
        public IActionResult GetAllMeetings(int pageNumber, string sortBy)
        {
            try
            {
                var meetings = _meetingService.GetAllMeetings(pageNumber, sortBy);
                return Ok(meetings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meetings. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// To get details of a particular Meeting
        /// </summary>
        [HttpGet("Meetings{id}")]
        public async Task<IActionResult> GetMeetingById(int id)
        {
            try
            {
                var meeting = await _meetingService.GetMeetingById(id);
                if (meeting == null)
                {
                    return NotFound();
                }
                return Ok(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meeting with ID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// To get all meetings of a particular employee
        /// </summary>
        [HttpGet("employee/{id}")]
        public IActionResult GetMeetingByEmployeeId([FromQuery][Required] int role, int id)
        {
            try
            {
                var meeting = _meetingService.GetMeetingByEmployeeId(id, role);
                if (meeting == null)
                {
                    return NotFound();
                }
                return Ok(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meeting with EmployeeID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// To get upcoming 3 meetings of a particular employee
        /// </summary>
        [HttpGet("employee/upcoming/meetings/{id}")]
        public IActionResult GetSoonMeetingByEmployeeId(int id)
        {
            try
            {
                var meeting = _meetingService.GetSoonMeetingByEmployeeId(id);
                if (meeting == null)
                {
                    return NotFound();
                }
                return Ok(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving soon meeting for employee with ID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// To create a new Meeting
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddMeeting(MeetingSchedule meeting)
        {
            try
            {
                await _meetingService.CreateMeeting(meeting);
                return CreatedAtAction(nameof(GetMeetingById), new { id = meeting.MeetingID }, meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while adding the meeting. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// To delete an Meeting
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            try
            {
                if (id != null)
                {
                    _meetingService.DeleteMeeting(id);
                    return NoContent();
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while deleting meeting with ID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// To get the meetings of an Employee Based on the program id.
        /// </summary>
        [HttpGet("meetings/programid/{id}")]
        public IActionResult GetMeetingsByProgramId(int id, int page, string? sortBy)
        {
            try
            {
                var tasks = _meetingService.GetMeetingsByProgramId(id, page, sortBy);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meeting for program with ID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

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
            try
            {
                var tasks = _meetingService.GetMeetingsByEmployeeId(id, page, sortBy);
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ERROR: An error occurred while retrieving meeting for employee with ID {id}. Exception: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }

        }
    }
}