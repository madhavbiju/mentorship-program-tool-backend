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

        // Get All Count
        public int GetAdminDashboardMenteeCount()
        {
            int MenteeCount = _context.Employees
                .Join(_context.EmployeeRoleMappings, e => e.EmployeeID, erm => erm.EmployeeID, (e, erm) => new { e, erm })
                .Join(_context.Roles, x => x.erm.RoleID, r => r.RoleID, (x, r) => new { x, r })
                .Where(x => x.r.RoleName == "Mentee" && x.x.e.AccountStatus == "active")
                .Count();

            return MenteeCount;
        }

        public int GetAdminDashboardProgramCount()
        {
            int ActivePairCount = _context.Programs.Count();
            return ActivePairCount;
        }

        public int GetAdminDashboardMentorCount()
        {
            int MentorCount = _context.Employees
                .Join(_context.EmployeeRoleMappings, e => e.EmployeeID, erm => erm.EmployeeID, (e, erm) => new { e, erm })
                .Join(_context.Roles, x => x.erm.RoleID, r => r.RoleID, (x, r) => new { x, r })
                .Where(x => x.r.RoleName == "Mentor" && x.x.e.AccountStatus == "active")
                .Count();

            return MentorCount;
        }
    }
}
