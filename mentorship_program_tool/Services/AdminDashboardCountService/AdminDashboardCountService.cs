using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Services.AdminDashboardCountService
{
    public class AdminDashboardCountService : IAdminDashboardCountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public AdminDashboardCountService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }


        //Get All Count
        public int GetAdminDashboardMenteeCount()
        {
            int MenteeCount = _context.Employee
         .Join(_context.employeerolemapping, e => e.employeeid, erm => erm.EmployeeId, (e, erm) => new { e, erm })
         .Join(_context.role, x => x.erm.RoleId, r => r.roleid, (x, r) => new { x, r })
         .Where(x => x.r.roles == "Mentee" && x.x.e.accountstatus == "active")
         .Count();

            return MenteeCount;

        }

        public int GetAdminDashboardProgramCount()
        {

            int ActivePairCount = _context.Program.Count();
            return ActivePairCount;

        }

        public int GetAdminDashboardMentorCount()
        {
            int MentorCount = _context.Employee
         .Join(_context.employeerolemapping, e => e.employeeid, erm => erm.EmployeeId, (e, erm) => new { e, erm })
         .Join(_context.role, x => x.erm.RoleId, r => r.roleid, (x, r) => new { x, r })
         .Where(x => x.r.roles == "Mentor" && x.x.e.accountstatus == "active")
         .Count();
            return MentorCount;
        }
    }
}
