using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using mentorship_program_tool.Services.MenteesOfMentorListService;
using mentorship_program_tool.Services.MentorsOfMenteesListService;

using mentorship_program_tool.Services.MentorDashboardCountService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using static Azure.Core.HttpHeader;
using mentorship_program_tool.Services.GetAllMenteesListService;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/mentee")]
    /*   [Authorize(Policy = "RequireMenteeRole")]*/
    public class MenteeController : ControllerBase
    {
        private readonly IGetAllMenteesOfMentorService _getAllMenteesOfMentorService;
        private readonly IGetAllActiveUnpairedMenteesService _getAllActiveUnpairedMenteesService;
        private readonly IGetMenteeDetailsByIdService _getMenteeDetailsByIdService;
        private readonly IMentorDashboardCountService _mentorDashboardCountService;
        private readonly IMenteesOfMentorListService _menteesOfMentorListService;
        private readonly IMentorsOfMenteesListService _mentorsOfMenteesListService;
        private readonly IGetAllMenteesListService _getAllMenteesListService;


        public MenteeController(IGetAllMenteesListService getAllMenteesListService, IGetAllMenteesOfMentorService getAllMenteesOfMentorService, IGetAllActiveUnpairedMenteesService GetAllActiveUnpairedMenteesService, IGetMenteeDetailsByIdService GetMenteeDetailsByIdService, IMentorDashboardCountService mentorDashboardCountService, IMenteesOfMentorListService menteesOfMentorListService, IMentorsOfMenteesListService mentorsOfMenteesListService)
        {
            _menteesOfMentorListService = menteesOfMentorListService;
            _getAllMenteesOfMentorService = getAllMenteesOfMentorService;
            _getAllActiveUnpairedMenteesService = GetAllActiveUnpairedMenteesService;
            _getMenteeDetailsByIdService = GetMenteeDetailsByIdService;
            _mentorDashboardCountService = mentorDashboardCountService;
            _mentorsOfMenteesListService = mentorsOfMenteesListService;
            _getAllMenteesListService= getAllMenteesListService;
        }

        /// <summary>
        /// To get all details of mentees under a mentor.
        /// </summary>
        [HttpGet("mentor/{id}")]
        public ActionResult<GetAllMenteesOfMentorResponseAPIModel> GetAllMenteesById(int id, [FromQuery] int pageNumber, [FromQuery] int pageSize, string? sortBy)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }

            var menteesList = _getAllMenteesOfMentorService.GetAllMenteesById(id, pageNumber, pageSize, sortBy);

            return Ok(menteesList);
        }

        /// <summary>
        /// To get list of mentees under Mentor
        /// </summary>
        [HttpGet("mentor/list/{id}")]
        public ActionResult<MenteesOfMentorListAPIModel> GetAllMenteesById(int id)
        {
            var menteesList = _menteesOfMentorListService.GetAllMenteesById(id);
            return Ok(menteesList);
        }

        /// <summary>
        /// To get list of all mentees
        /// </summary>
        [HttpGet("all/list/")]
        public ActionResult<MenteesOfMentorListAPIModel> GetAllMenteesList()
        {
            var menteesList = _getAllMenteesListService.GetAllMenteesList();
            return Ok(menteesList);
        }

        /// <summary>
        /// To get list of Mentors of Mentee
        /// </summary>
        [HttpGet("mentees/list/{id}")]
        public ActionResult<MentorsOfMenteesListAPImodel> GetAllMentorsById(int id)
        {
            var mentorsList = _mentorsOfMenteesListService.GetAllMentorsById(id);
            return Ok(mentorsList);
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
            return Ok(details);
        }
    }
}
