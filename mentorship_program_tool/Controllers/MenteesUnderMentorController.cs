using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/MenteesUnderMentor")]
    public class MenteesUnderMentorController : ControllerBase
    {
        private readonly IGetAllMenteesOfMentorService _getAllMenteesOfMentorService;

        public MenteesUnderMentorController(IGetAllMenteesOfMentorService getAllMenteesOfMentorService)
        {
            _getAllMenteesOfMentorService = getAllMenteesOfMentorService;
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<GetAllMenteesOfMentorAPIModel> GetAllMenteesById(int id)
        {
            var menteesList = _getAllMenteesOfMentorService.GetAllMenteesById(id);
            if (menteesList == null)
                return NotFound();
            return Ok(menteesList);
        }
    }
}
