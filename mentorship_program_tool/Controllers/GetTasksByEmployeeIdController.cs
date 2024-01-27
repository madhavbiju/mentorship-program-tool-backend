using Microsoft.AspNetCore.Mvc;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.Services.GetTasksbyEmployeeIdService;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GetTasksByEmployeeIdController : ControllerBase
    {
       
      
            private readonly IGetTasksbyEmployeeIdService _getTasksByEmployeeIdService;

            public GetTasksByEmployeeIdController(IGetTasksbyEmployeeIdService GetTasksByEmployeeIdService)
            {
            _getTasksByEmployeeIdService = GetTasksByEmployeeIdService;
            }

        //getall Pending request
        [HttpGet("{id},{status}")]
        public IActionResult GetTasksByEmployeeId(int id, int status)
            {
                var tasks = _getTasksByEmployeeIdService.GetTasksByEmployeeId(id,status);
                return Ok(tasks);

            }
        }
    
}
