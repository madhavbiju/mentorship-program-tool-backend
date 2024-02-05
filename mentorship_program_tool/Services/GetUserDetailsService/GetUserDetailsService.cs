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

        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName == role)?.RoleID;

            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            // First, get the total count of users with the specified role (before applying pagination)
            var totalCount = _context.Employees
                .Join(_context.EmployeeRoleMappings, e => e.EmployeeID, erm => erm.EmployeeID, (e, erm) => erm)
                .Count(erm => erm.RoleID == roleId);

            // Calculate the number of records to skip for pagination
            int skip = (pageNumber - 1) * pageSize;

            var userList = _context.Employees
                .Join(_context.EmployeeRoleMappings, e => e.EmployeeID, erm => erm.EmployeeID, (e, erm) => new { Employee = e, EmployeeRoleMapping = erm })
                .Where(x => x.EmployeeRoleMapping.RoleID == roleId)
                .Select(x => new GetUserDetailsAPIModel
                {
                    UserID = x.Employee.EmployeeID,
                    UserName = x.Employee.FirstName + " " + x.Employee.LastName,
                    UserRole = role,
                    UserJob = "Lead Tester",
                    UserStatus = x.Employee.AccountStatus
                })
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return new UserDetailsResponseAPIModel { Users = userList, TotalCount = totalCount };
        }


    }
}
