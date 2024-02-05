using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.GetAllProgramService;
using mentorship_program_tool.Services.ProgramService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IGetAllProgramsService _getAllProgramsService;


        public ProgramController(IProgramService programService, IGetAllProgramsService GetAllProgramsService)
        {
            _programService = programService;
            _getAllProgramsService = GetAllProgramsService;
        }

        [HttpGet]
        public IActionResult GetPrograms()
        {
            var program = _programService.GetProgram();
            return Ok(program);
        }

        [HttpGet("All")]
        public IActionResult GetAllPrograms()
        {
            var programs = _getAllProgramsService.GetAllPrograms();
            return Ok(programs);

        }


        [HttpGet("{id}")]
        public IActionResult GetProgramsById(int id)
        {
            var program = _programService.GetProgramById(id);
            if (program == null)
            {
                return NotFound();
            }
            return Ok(program);
        }

        [HttpPost]
        public IActionResult AddPrograms(Models.EntityModel.Program program)
        {
            _programService.CreateProgram(program);
            return CreatedAtAction(nameof(GetProgramsById), new { id = program.ProgramID }, program);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePrograms(int id, Models.EntityModel.Program program)
        {
            if (id != program.ProgramID)
            {
                return BadRequest();
            }

            _programService.UpdateProgram(id, program);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePrograms(int id)
        {
            if (id != null)
            {
                _programService.DeleteProgram(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}
