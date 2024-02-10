using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using mentorship_program_tool.Services.MentorDashboardCountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using static Azure.Core.HttpHeader;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/mentee")]
    [Authorize(Policy = "RequireMenteeRole")]
    public class MenteeController : ControllerBase
    {
        private readonly IGetAllMenteesOfMentorService _getAllMenteesOfMentorService;
        private readonly IGetAllActiveUnpairedMenteesService _getAllActiveUnpairedMenteesService;
        private readonly IGetMenteeDetailsByIdService _getMenteeDetailsByIdService;
        private readonly IMentorDashboardCountService _mentorDashboardCountService;

        public MenteeController(IGetAllMenteesOfMentorService getAllMenteesOfMentorService, IGetAllActiveUnpairedMenteesService GetAllActiveUnpairedMenteesService, IGetMenteeDetailsByIdService GetMenteeDetailsByIdService, IMentorDashboardCountService mentorDashboardCountService)
        {
            _getAllMenteesOfMentorService = getAllMenteesOfMentorService;
            _getAllActiveUnpairedMenteesService = GetAllActiveUnpairedMenteesService;
            _getMenteeDetailsByIdService = GetMenteeDetailsByIdService;
            _mentorDashboardCountService = mentorDashboardCountService;
        }

        /// <summary>
        /// To get all mentees under a mentor.
        /// </summary>
        [HttpGet("mentor/{id}")]
        public ActionResult<GetAllMenteesOfMentorResponseAPIModel> GetAllMenteesById(int id, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            var menteesList = _getAllMenteesOfMentorService.GetAllMenteesById(id, pageNumber, pageSize);

            if (menteesList == null)
            {
                return NotFound();
            }

            return Ok(menteesList);
        }


        /// <summary>
        /// To get count of mentees under a mentor.
        /// </summary>
        [HttpGet("mentees-count-under-mentor/{id}")]
        public IActionResult GetDashboardCount(int id)
        {
            MentorDashboardCountAPIModel mentees = new MentorDashboardCountAPIModel();
            mentees.MenteeCount = _mentorDashboardCountService.GetMentorDashboardMenteeCount(id);
            return Ok(mentees);
        }

        /// <summary>
        /// To get all mentees who are active and unpaired
        /// </summary>
        [HttpGet("active-unpaired")]
        public IActionResult GetAllActiveUnpairedMentees()
        {
            var programs = _getAllActiveUnpairedMenteesService.GetAllActiveUnpairedMentees();
            return Ok(programs);

        }

        /// <summary>
        /// To get all details of a mentee
        /// </summary>
        [HttpGet("details/{id}")]
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
