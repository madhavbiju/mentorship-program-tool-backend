using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllMenteesOfMentorService;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetAllMenteesOfMentorController : ControllerBase
    {
        private readonly IGetAllMenteesOfMentorService _getAllMenteesOfMentorService;

        public GetAllMenteesOfMentorController(IGetAllMenteesOfMentorService getAllMenteesOfMentorService)
        {
            _getAllMenteesOfMentorService = getAllMenteesOfMentorService;
        }


        [HttpGet("using/{id}")]
        public ActionResult<GetAllMenteesOfMentorAPIModel> GetAllMenteesById(int id)
        {
            var menteesList = _getAllMenteesOfMentorService.GetAllMenteesById(id);
            if (menteesList == null)
                return NotFound();
            return Ok(menteesList);
        }
    }
}
