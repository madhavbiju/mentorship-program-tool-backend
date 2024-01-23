using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetAllActiveUnpairedMenteesController : ControllerBase
    {
        private readonly IGetAllActiveUnpairedMenteesService  _getAllActiveUnpairedMenteesService;

        public GetAllActiveUnpairedMenteesController(IGetAllActiveUnpairedMenteesService  GetAllActiveUnpairedMenteesService)
        {
             _getAllActiveUnpairedMenteesService =  GetAllActiveUnpairedMenteesService;
        }

        //getall Pending request
        [HttpGet]
        public IActionResult  GetAllActiveUnpairedMentees()
        {
            var programs =  _getAllActiveUnpairedMenteesService.GetAllActiveUnpairedMentees();
            return Ok(programs);

        }
    }
}


