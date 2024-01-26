using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.ProgramService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        [HttpGet]
        public IActionResult GetPrograms()
        {
            var program = _programService.GetProgram();
            return Ok(program);
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
        public IActionResult AddPrograms(ProgramModel program)
        {
            _programService.CreateProgram(program);
            return CreatedAtAction(nameof(GetProgramsById), new { id = program.programid }, program);
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
