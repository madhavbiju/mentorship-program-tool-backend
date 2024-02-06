using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using System.Linq;

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
            IQueryable<EmployeeRoleMapping> roleMappingsQuery;
            int? roleId = null;
            if (role.ToLower() != "all")
            {
                roleId = _context.Roles.FirstOrDefault(r => r.RoleName == role)?.RoleID;
                if (roleId == null)
                {
                    return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
                }
                roleMappingsQuery = _context.EmployeeRoleMappings.Where(erm => erm.RoleID == roleId);
            }
            else
            {
                roleMappingsQuery = _context.EmployeeRoleMappings;
            }

            // First, get the total count of users with the specified role (before applying pagination)
            var totalCount = roleMappingsQuery
                .Join(_context.Employees, erm => erm.EmployeeID, e => e.EmployeeID, (erm, e) => e)
                .Count();

            // Calculate the number of records to skip for pagination
            int skip = (pageNumber - 1) * pageSize;

            var userListQuery = roleMappingsQuery
                .Join(_context.Employees, erm => erm.EmployeeID, e => e.EmployeeID, (erm, e) => new { Employee = e, EmployeeRoleMapping = erm })
                .Select(x => new GetUserDetailsAPIModel
                {
                    UserID = x.Employee.EmployeeID,
                    UserName = x.Employee.FirstName + " " + x.Employee.LastName,
                    UserRole = roleId.HasValue ? role : _context.Roles.FirstOrDefault(r => r.RoleID == x.EmployeeRoleMapping.RoleID).RoleName,
                    UserJob = "Lead Tester", // Assuming static, adjust as necessary
                    UserStatus = x.Employee.AccountStatus
                });

            var userList = userListQuery
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            return new UserDetailsResponseAPIModel { Users = userList, TotalCount = totalCount };
        }
    }
}
