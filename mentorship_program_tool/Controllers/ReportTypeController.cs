﻿using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportTypeController : ControllerBase
    {
        private readonly IReportTypeService _ReportTypeService;

        public ReportTypeController(IReportTypeService ReportTypeService)
        {
            _ReportTypeService = ReportTypeService;
        }

        [HttpGet]
        public IActionResult GetReportTypes()
        {
            var ReportType = _ReportTypeService.GetReportType();
            return Ok(ReportType);
        }

        [HttpGet("{id}")]
        public IActionResult GetReportTypeById(int id)
        {
            var ReportType = _ReportTypeService.GetReportTypeById(id);
            if (ReportType == null)
            {
                return NotFound();
            }
            return Ok(ReportType);
        }

        [HttpPost]
        public IActionResult AddReportType(ReportTypeModel ReportType)
        {
            _ReportTypeService.CreateReportType(ReportType);
            return CreatedAtAction(nameof(GetReportTypeById), new { id = ReportType.reporttypeid }, ReportType);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReportType(int id, ReportTypeModel ReportType)
        {
            if (id != ReportType.reporttypeid)
            {
                return BadRequest();
            }

            _ReportTypeService.UpdateReportType(id, ReportType);
            return NoContent();
        }

        [HttpDelete("{id}")]
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
