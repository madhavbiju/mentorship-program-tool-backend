using Microsoft.AspNetCore.Mvc;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetTasksByProgramIdController : ControllerBase
    {
       
      
            private readonly IGetTasksByProgramIdService _getTasksByProgramIdService;

            public GetTasksByProgramIdController(IGetTasksByProgramIdService GetTasksByProgramIdService)
            {
            _getTasksByProgramIdService = GetTasksByProgramIdService;
            }

        //getall Pending request
        [HttpGet("{id},{status}")]
        public IActionResult GetTasksByProgramId(int id, int status)
            {
                var tasks = _getTasksByProgramIdService.GetTasksByProgramId(id,status);
                return Ok(tasks);

            }
        }
    
}
