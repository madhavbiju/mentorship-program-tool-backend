/*using mentorship_program_tool.Data;
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
            IQueryable<EmployeeRoleMapping> roleMappingsQuery = _context.EmployeeRoleMappings;

            if (role.ToLower() != "assigned")
            {
                var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
                if (roleId == null)
                {
                    return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
                }
                roleMappingsQuery = roleMappingsQuery.Where(erm => erm.RoleID == roleId.Value);
            }

            var employeeDetailsQuery = from erm in roleMappingsQuery
                                       join e in _context.Employees on erm.EmployeeID equals e.EmployeeID
                                       join r in _context.Roles on erm.RoleID equals r.RoleID
                                       select new
                                       {
                                           e.EmployeeID,
                                           e.FirstName,
                                           e.LastName,
                                           e.AccountStatus,
                                           RoleName = r.RoleName
                                       };

            var userData = employeeDetailsQuery.ToList();

            var groupedData = userData
                .GroupBy(e => new { e.EmployeeID, e.FirstName, e.LastName, e.AccountStatus })
                .Select(g => new GetUserDetailsAPIModel
                {
                    UserID = g.Key.EmployeeID,
                    UserName = $"{g.Key.FirstName} {g.Key.LastName}",
                    UserRoles = g.Select(x => x.RoleName).Distinct().ToList(),
                    UserJob = "Lead Tester", // Assuming this needs to be dynamically fetched or set
                    UserStatus = g.Key.AccountStatus
                });

            // Pagination
            int totalCount = groupedData.Count();
            var paginatedData = groupedData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new UserDetailsResponseAPIModel { Users = paginatedData, TotalCount = totalCount };
        }
    }
}*/

/*using mentorship_program_tool.Data;
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
            IQueryable<Employee> baseQuery = _context.Employees;

            if (role.ToLower() == "unassigned")
            {
                // For "unassigned", filter employees not linked to any roles
                baseQuery = baseQuery.Where(e => !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.EmployeeID));
            }
            else if (role.ToLower() != "all")
            {
                // For specific roles, first find the roleId, then filter employees linked to that role
                var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
                if (!roleId.HasValue)
                {
                    return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
                }

                baseQuery = from e in _context.Employees
                            join erm in _context.EmployeeRoleMappings on e.EmployeeID equals erm.EmployeeID
                            where erm.RoleID == roleId
                            select e;
            }

            // Project the query to include user details and roles
            var userDetailsQuery = from e in baseQuery
                                   select new GetUserDetailsAPIModel
                                   {
                                       UserID = e.EmployeeID,
                                       UserName = e.FirstName + " " + e.LastName,
                                       UserRoles = (from erm in _context.EmployeeRoleMappings
                                                    join r in _context.Roles on erm.RoleID equals r.RoleID
                                                    where erm.EmployeeID == e.EmployeeID
                                                    select r.RoleName).Distinct().ToList(),
                                       UserJob = "Lead Tester", // Example static value, replace as needed
                                       UserStatus = e.AccountStatus
                                   };

            // Pagination should be applied after projecting to the final model
            var paginatedUserDetails = userDetailsQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = userDetailsQuery.Count(); // Get total count for pagination metadata

            return new UserDetailsResponseAPIModel
            {
                Users = paginatedUserDetails,
                TotalCount = totalCount
            };
        }
    }
}
*/

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
            if (role.ToLower() == "all")
            {
                var allUsersQuery = from e in _context.Employees
                                    select new GetUserDetailsAPIModel
                                    {
                                        UserID = e.EmployeeID,
                                        UserName = $"{e.FirstName} {e.LastName}",
                                        UserRoles = (from erm in _context.EmployeeRoleMappings
                                                     join r in _context.Roles on erm.RoleID equals r.RoleID
                                                     where erm.EmployeeID == e.EmployeeID
                                                     select r.RoleName).Distinct().ToList(),
                                        UserJob = "Lead Tester", // Example static value, replace as needed
                                        UserStatus = e.AccountStatus
                                    };

                var paginatedAllUsers = allUsersQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                var totalCountAll = allUsersQuery.Count();

                return new UserDetailsResponseAPIModel { Users = paginatedAllUsers, TotalCount = totalCountAll };
            }

            if (role.ToLower() == "unassigned")
            {
                var unassignedUsersQuery = from e in _context.Employees
                                           where !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.EmployeeID)
                                           select new GetUserDetailsAPIModel
                                           {
                                               UserID = e.EmployeeID,
                                               UserName = $"{e.FirstName} {e.LastName}",
                                               UserRoles = new List<string>(), // No roles
                                               UserJob = "Lead Tester", // Example static value, replace as needed
                                               UserStatus = e.AccountStatus
                                           };

                var paginatedUnassignedUsers = unassignedUsersQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                var totalCountUnassigned = unassignedUsersQuery.Count();

                return new UserDetailsResponseAPIModel { Users = paginatedUnassignedUsers, TotalCount = totalCountUnassigned };
            }

            IQueryable<EmployeeRoleMapping> roleMappingsQuery = _context.EmployeeRoleMappings;

            if (role.ToLower() != "assigned")
            {
                var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
                if (roleId == null)
                {
                    return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
                }
                roleMappingsQuery = roleMappingsQuery.Where(erm => erm.RoleID == roleId.Value);
            }

            var employeeDetailsQuery = from erm in roleMappingsQuery
                                       join e in _context.Employees on erm.EmployeeID equals e.EmployeeID
                                       join r in _context.Roles on erm.RoleID equals r.RoleID
                                       select new
                                       {
                                           e.EmployeeID,
                                           e.FirstName,
                                           e.LastName,
                                           e.AccountStatus,
                                           RoleName = r.RoleName
                                       };

            var userData = employeeDetailsQuery.ToList();

            var groupedData = userData
                .GroupBy(e => new { e.EmployeeID, e.FirstName, e.LastName, e.AccountStatus })
                .Select(g => new GetUserDetailsAPIModel
                {
                    UserID = g.Key.EmployeeID,
                    UserName = $"{g.Key.FirstName} {g.Key.LastName}",
                    UserRoles = g.Select(x => x.RoleName).Distinct().ToList(),
                    UserJob = "Lead Tester", // Assuming this needs to be dynamically fetched or set
                    UserStatus = g.Key.AccountStatus
                });

            int totalCount = groupedData.Count();
            var paginatedData = groupedData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new UserDetailsResponseAPIModel { Users = paginatedData, TotalCount = totalCount };
        }
    }
}


