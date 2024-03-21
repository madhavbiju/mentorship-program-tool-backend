using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.ReportTypeService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportTypeController : ControllerBase
    {
        private readonly IReportTypeService _ReportTypeService;

        public ReportTypeController(IReportTypeService ReportTypeService)
        {
            _ReportTypeService = ReportTypeService;
        }

        /// <summary>
        /// To get all report type ids
        /// </summary>
        [HttpGet("typeid/all/{id}")]
        public async Task<IActionResult> GetReportTypes()
        {
            var ReportType = await _ReportTypeService.GetReportType();
            return Ok(ReportType);
        }

        /// <summary>
        /// To get one report type id
        /// </summary>

        [HttpGet("typeid/{id}")]
        public async Task<IActionResult> GetReportTypeById(int id)
        {
            var ReportType = await _ReportTypeService.GetReportTypeById(id);
            if (ReportType == null)
            {
                return NotFound();
            }
            return Ok(ReportType);
        }

        /// <summary>
        /// To post a new report type id
        /// </summary>
        [HttpPost("typeid")]
        public async Task<IActionResult> AddReportType(ReportType ReportType)
        {
            await _ReportTypeService.CreateReportType(ReportType);
            return CreatedAtAction(nameof(GetReportTypeById), new { id = ReportType.ReportTypeID }, ReportType);
        }

        /// <summary>
        /// To modify a new report type id
        /// </summary>
        [HttpPut("typeid/{id}")]
        public IActionResult UpdateReportType(int id, ReportType ReportType)
        {
            if (id != ReportType.ReportTypeID)
            {
                return BadRequest();
            }

            _ReportTypeService.UpdateReportType(id, ReportType);
            return NoContent();
        }


        /// <summary>
        /// To delete a new report type id
        /// </summary>
        [HttpDelete("typeid{id}")]
        public IActionResult DeleteReportType(int id)
        {
            if (id != null)
            {
                _ReportTypeService.DeleteReport(id);
                return NoContent();
            }

            return NotFound();
        }
    }
}
