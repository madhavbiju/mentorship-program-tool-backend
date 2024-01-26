using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetUserDetailsService
{
    public class GetUserDetailsService : IGetUserDetailsService
    {
        private readonly AppDbContext _context;

        public GetUserDetailsService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<GetUserDetailsAPIModel> GetUserDetails(string role)
        {
            var roleId = _context.role.FirstOrDefault(r => r.roles == role)?.roleid;

            if (roleId == null)
            {
                return Enumerable.Empty<GetUserDetailsAPIModel>();
            }

            var userList = _context.Employee
                .Join(_context.employeerolemapping, e => e.employeeid, erm => erm.EmployeeId, (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
                .Where(x => x.EmployeeRoleMapping.RoleId == roleId)
                .Select(x => new GetUserDetailsAPIModel
                {
                    UserID = x.Employee.employeeid,
                    UserName = x.Employee.firstname + " " + x.Employee.lastname,
                    UserRole = role,
                    UserJob = "Lead Tester",
                    UserStatus = x.Employee.accountstatus
                    // Add other properties from Employee or related entities as needed
                })
                .ToList();

            return userList;
        }
    }
}
