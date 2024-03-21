using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.StatusService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public IActionResult GetStatuss()
        {
            var status = _statusService.GetStatus();
            return Ok(status);
        }

        [HttpGet("{id}")]
        public IActionResult GetStatusById(int id)
        {
            var status = _statusService.GetStatusById(id);
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public IActionResult AddStatus(Status status)
        {
            _statusService.CreateStatus(status);
            return CreatedAtAction(nameof(GetStatusById), new { id = status.StatusID }, status);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStatus(int id, Status status)
        {
            if (id != status.StatusID)
            {
                return BadRequest();
            }

            _statusService.UpdateStatus(id, status);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStatus(int id)
        {
            if (id != null)
            {
                _statusService.DeleteStatus(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}
