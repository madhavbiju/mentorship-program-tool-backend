using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetAllProgramsController : ControllerBase
    {
        private readonly IGetAllProgramsService _getAllProgramsService;

        public GetAllProgramsController(IGetAllProgramsService GetAllProgramsService)
        {
            _getAllProgramsService = GetAllProgramsService;
        }

        //getall Pending request
        [HttpGet("GetAllPrograms")]
        public IActionResult GetAllPrograms()
        {
            var programs = _getAllProgramsService.GetAllPrograms();
            return Ok(programs);

        }
    }
}


