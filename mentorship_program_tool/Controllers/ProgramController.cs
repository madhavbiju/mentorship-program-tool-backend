using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.GetAllProgramService;
using mentorship_program_tool.Services.ProgramService;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Microsoft.EntityFrameworkCore;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.PutProgramExtensionService;

namespace mentorship_program_tool.Controllers
{


    [ApiController]
    [Route("api/program")]
    public class ProgramController : ControllerBase
    {
        private readonly IProgramService _programService;
        private readonly IGetAllProgramsService _getAllProgramsService;
        private readonly IProgramDateExtensionService _programDateExtensionService;


        public ProgramController(IProgramService programService, IGetAllProgramsService GetAllProgramsService, IProgramDateExtensionService programDateExtensionService)
        {
            _programService = programService;
            _getAllProgramsService = GetAllProgramsService;
            _programDateExtensionService = programDateExtensionService;
        }

        /// <summary>
        /// To get details of all programs
        /// </summary>
        [HttpGet]
        public IActionResult GetPrograms([FromQuery][Required] int status, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {

            // Validate pageNumber and pageSize
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("PageNumber and PageSize must be greater than 0.");
            }
            var program = _programService.GetProgram(status, pageNumber, pageSize);
            return Ok(program);
        }

        [HttpGet("All")]
        public IActionResult GetAllPrograms(int page = 1, [FromQuery] int? programStatus = null, [FromQuery] string sortOrder = "programName", [FromQuery] string search = null)
        {
            try
            {
                var programs = _getAllProgramsService.GetAllPrograms(page, programStatus, sortOrder, search);

                return Ok(programs);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "An error occurred while retrieving programs.");
            }
        }

        [HttpGet("ending-soon")]
        public IActionResult GetAllProgramsEndingSoon([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var programs = _getAllProgramsService.GetAllProgramsEndingSoon(pageNumber, pageSize);
            return Ok(programs);

        }

        [HttpPut("UpdateProgramDate")]
        public async Task<IActionResult> UpdateProgramDate([FromBody] ProgramDateUpdateAPIModel model)
        {
            var result = await _programDateExtensionService.UpdateProgramDateAsync(model);
            if (result)
                return Ok(new { message = "Program date updated successfully" });
            else
                return NotFound(new { message = "Program not found" });
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
