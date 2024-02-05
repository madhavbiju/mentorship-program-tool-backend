using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.ProgramService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    

    [ApiController]
    [Route("api/program")]
public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        /// <summary>
        /// To get details of all programs
        /// </summary>
        [HttpGet]
        public IActionResult GetPrograms()
        {
            var program = _programService.GetProgram();
            return Ok(program);
        }

        /// <summary>
        /// To get details of a particular program
        /// </summary>
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

        /// <summary>
        /// To create a new program
        /// </summary>
        [HttpPost]
        public IActionResult AddPrograms(Models.EntityModel.Program program)
        {
            _programService.CreateProgram(program);
            return CreatedAtAction(nameof(GetProgramsById), new { id = program.ProgramID }, program);
        }

        /// <summary>
        /// To edit a program
        /// </summary>
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

        /// <summary>
        /// To delete a program
        /// </summary>
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
