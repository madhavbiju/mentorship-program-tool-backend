using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using mentorship_program_tool.Services.GetMenteeDetailsById;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/mentee")]
    public class MenteeController : ControllerBase
    {
        private readonly IGetAllMenteesOfMentorService _getAllMenteesOfMentorService;
        private readonly IGetAllActiveUnpairedMenteesService _getAllActiveUnpairedMenteesService;
        private readonly IGetMenteeDetailsByIdService _getMenteeDetailsByIdService;

        public MenteeController(IGetAllMenteesOfMentorService getAllMenteesOfMentorService, IGetAllActiveUnpairedMenteesService GetAllActiveUnpairedMenteesService, IGetMenteeDetailsByIdService GetMenteeDetailsByIdService)
        {
            _getAllMenteesOfMentorService = getAllMenteesOfMentorService;
            _getAllActiveUnpairedMenteesService = GetAllActiveUnpairedMenteesService;
            _getMenteeDetailsByIdService = GetMenteeDetailsByIdService;
        }

        /// <summary>
        /// To get all mentees under a mentor.
        /// </summary>
        [HttpGet("mentor/{id}")]
        public ActionResult<GetAllMenteesOfMentorAPIModel> GetAllMenteesById(int id)
        {
            var menteesList = _getAllMenteesOfMentorService.GetAllMenteesById(id);
            if (menteesList == null)
                return NotFound();
            return Ok(menteesList);
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
