using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetAllProgramService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetActiveTasksController : ControllerBase
    {
       
      
            private readonly IGetActiveTasksService _getActiveTasksService;

            public GetActiveTasksController(IGetActiveTasksService GetActiveTasksService)
            {
            _getActiveTasksService = GetActiveTasksService;
            }

            //getall Pending request
            [HttpGet("GetAllTasks")]
            public IActionResult GetAllActiveTasks()
            {
                var tasks = _getActiveTasksService.GetAllActiveTasks();
                return Ok(tasks);

            }
        }
    
}
