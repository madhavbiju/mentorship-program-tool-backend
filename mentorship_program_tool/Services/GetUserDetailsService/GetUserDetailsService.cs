using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using System.Collections.Generic;
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
            if (role.ToLower() != "all")
            {
                var roleId = _context.Roles.FirstOrDefault(r => r.RoleName == role)?.RoleID;
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

            // This time, let's group by employee to handle multiple roles
            var groupedQuery = roleMappingsQuery
                .Join(_context.Employees, erm => erm.EmployeeID, e => e.EmployeeID, (erm, e) => new { e.EmployeeID, e.FirstName, e.LastName, e.AccountStatus, RoleID = erm.RoleID })
                .GroupBy(x => new { x.EmployeeID, x.FirstName, x.LastName, x.AccountStatus })
                .Select(g => new GetUserDetailsAPIModel
                {
                    UserID = g.Key.EmployeeID,
                    UserName = g.Key.FirstName + " " + g.Key.LastName,
                    UserRoles = g.Select(x => _context.Roles.FirstOrDefault(r => r.RoleID == x.RoleID).RoleName).Distinct().ToList(),
                    UserJob = "Lead Tester", // Assuming static, adjust as necessary
                    UserStatus = g.Key.AccountStatus
                });

            // Apply pagination after grouping and collecting roles
            int skip = (pageNumber - 1) * pageSize;
            var totalCount = groupedQuery.Count();
            var userList = groupedQuery.Skip(skip).Take(pageSize).ToList();

            return new UserDetailsResponseAPIModel { Users = userList, TotalCount = totalCount };
        }
    }
}
