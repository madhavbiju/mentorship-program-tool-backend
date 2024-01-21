using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{
    
        [ApiController]
        [Route("api/[controller]")]
    public class AdminDashBoardCountController : ControllerBase
        {
            private readonly IAdminDashboardCountService _adminDashboardCountService;

            public AdminDashBoardCountController(IAdminDashboardCountService adminDashboardCountService)
            {
            _adminDashboardCountService = adminDashboardCountService;
            }

            //getall Pending request
            [HttpGet("Get Admin Dashboard Count")]
            public IActionResult GetDashboardCount()
            {
            AdminDashboardCountAPIModel admin=new AdminDashboardCountAPIModel();
            admin.MenteeCount = _adminDashboardCountService.GetAdminDashboardMenteeCount();
            admin.MentorCount = _adminDashboardCountService.GetAdminDashboardMentorCount();
            admin.ActivePairCount = _adminDashboardCountService.GetAdminDashboardProgramPairCount();
                return Ok(admin);
            }
        }
    }


