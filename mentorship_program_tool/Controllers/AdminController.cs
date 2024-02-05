using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.AdminDashboardCountService;
using Microsoft.AspNetCore.Mvc;

namespace mentorship_program_tool.Controllers
{

    [ApiController]
        [Route("api/admin")]
    public class AdminController : ControllerBase
        {
            private readonly IAdminDashboardCountService _adminDashboardCountService;

            public AdminController(IAdminDashboardCountService adminDashboardCountService)
            {
            _adminDashboardCountService = adminDashboardCountService;
            }

        /// <summary>
        /// To get count of Active Mentors,Mentees and Pairs
        /// </summary>
        //getall Pending request
        [HttpGet("active-count")]
            public IActionResult GetDashboardCount()
            {
            AdminDashboardCountAPIModel admin=new AdminDashboardCountAPIModel();
            admin.MenteeCount = _adminDashboardCountService.GetAdminDashboardMenteeCount();
            admin.MentorCount = _adminDashboardCountService.GetAdminDashboardMentorCount();
            admin.ActivePairCount = _adminDashboardCountService.GetAdminDashboardProgramCount();
                return Ok(admin);
            }
        }
    }


