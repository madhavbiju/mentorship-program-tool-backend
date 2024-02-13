/*using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
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
            switch (role.ToLower())
            {
                case "all":
                    return GetAllUsers(pageNumber, pageSize);
                case "unassigned":
                    return GetUnassignedUsers(pageNumber, pageSize);
                case "assigned":
                    return GetAssignedUsers(pageNumber, pageSize);
                default:
                    return GetUsersBySpecificRole(role, pageNumber, pageSize);
            }
        }

        private UserDetailsResponseAPIModel GetAllUsers(int pageNumber, int pageSize)
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

            return PaginateUsers(allUsersQuery, pageNumber, pageSize);
        }

        private UserDetailsResponseAPIModel GetUnassignedUsers(int pageNumber, int pageSize)
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

            return PaginateUsers(unassignedUsersQuery, pageNumber, pageSize);
        }

        private UserDetailsResponseAPIModel GetAssignedUsers(int pageNumber, int pageSize)
        {
            var assignedUsersQuery = from e in _context.Employees
                                     where _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.EmployeeID)
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

            return PaginateUsers(assignedUsersQuery, pageNumber, pageSize);
        }

        private UserDetailsResponseAPIModel GetUsersBySpecificRole(string role, int pageNumber, int pageSize)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            var usersWithRoleQuery = from e in _context.Employees
                                     join erm in _context.EmployeeRoleMappings on e.EmployeeID equals erm.EmployeeID
                                     where erm.RoleID == roleId
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

            return PaginateUsers(usersWithRoleQuery, pageNumber, pageSize);
        }

        private UserDetailsResponseAPIModel PaginateUsers(IQueryable<GetUserDetailsAPIModel> query, int pageNumber, int pageSize)
        {
            var paginatedUsers = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = query.Count();

            return new UserDetailsResponseAPIModel { Users = paginatedUsers, TotalCount = totalCount };
        }
    }
}
*/
/*using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using System;
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

        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            switch (role.ToLower())
            {
                case "all":
                    return GetAllUsers(pageNumber, pageSize, sortParameter, isAscending);
                case "unassigned":
                    return GetUnassignedUsers(pageNumber, pageSize, sortParameter, isAscending);
                case "assigned":
                    return GetAssignedUsers(pageNumber, pageSize, sortParameter, isAscending);
                default:
                    return GetUsersBySpecificRole(role, pageNumber, pageSize, sortParameter, isAscending);
            }
        }

        private UserDetailsResponseAPIModel GetAllUsers(int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            var query = GetBaseUserQuery();
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, isAscending);
        }

        private UserDetailsResponseAPIModel GetUnassignedUsers(int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            var query = GetBaseUserQuery().Where(e => !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, isAscending);
        }

        private UserDetailsResponseAPIModel GetAssignedUsers(int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            var query = GetBaseUserQuery().Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, isAscending);
        }

        private UserDetailsResponseAPIModel GetUsersBySpecificRole(string role, int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            var query = GetBaseUserQuery().Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID && erm.RoleID == roleId));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, isAscending);
        }

        private IQueryable<GetUserDetailsAPIModel> GetBaseUserQuery()
        {
            return from e in _context.Employees
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
        }

        private UserDetailsResponseAPIModel PaginateAndSortUsers(IQueryable<GetUserDetailsAPIModel> query, int pageNumber, int pageSize, string sortParameter, bool isAscending)
        {
            // Fetch the data first
            var fetchedData = query.ToList();

            // Apply sorting in-memory
            IEnumerable<GetUserDetailsAPIModel> sortedData;
            switch (sortParameter.ToLower())
            {
                case "username":
                    sortedData = isAscending ?
                        fetchedData.OrderBy(u => u.UserName) :
                        fetchedData.OrderByDescending(u => u.UserName);
                    break;
                // Add more cases for other sortParameters if needed
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }

            // Now apply pagination
            var paginatedUsers = sortedData
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            var totalCount = fetchedData.Count;

            return new UserDetailsResponseAPIModel { Users = paginatedUsers, TotalCount = totalCount };
        }

    }
}*/
using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using System;
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

        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            switch (role.ToLower())
            {
                case "all":
                    return GetAllUsers(pageNumber, pageSize, sortParameter, sortType);
                case "unassigned":
                    return GetUnassignedUsers(pageNumber, pageSize, sortParameter, sortType);
                case "assigned":
                    return GetAssignedUsers(pageNumber, pageSize, sortParameter, sortType);
                default:
                    return GetUsersBySpecificRole(role, pageNumber, pageSize, sortParameter, sortType);
            }
        }

        private UserDetailsResponseAPIModel GetAllUsers(int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var query = GetBaseUserQuery();
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUnassignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var query = GetBaseUserQuery().Where(e => !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetAssignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var query = GetBaseUserQuery().Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUsersBySpecificRole(string role, int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            var query = GetBaseUserQuery().Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID && erm.RoleID == roleId));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private IQueryable<GetUserDetailsAPIModel> GetBaseUserQuery()
        {
            return from e in _context.Employees
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
        }

        private UserDetailsResponseAPIModel PaginateAndSortUsers(IQueryable<GetUserDetailsAPIModel> query, int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            // Assuming sortParameter and sortType have been validated

            // Fetch the data first (before sorting in-memory might be needed)
            var users = query.ToList(); // Materialize query

            // Determine sort direction and apply in-memory sorting if necessary
            bool isAscending = sortType.Equals("Asc", StringComparison.OrdinalIgnoreCase);
            switch (sortParameter.ToLower())
            {
                case "username":
                    if (isAscending)
                    {
                        users = users.OrderBy(u => u.UserName).ToList();
                    }
                    else
                    {
                        users = users.OrderByDescending(u => u.UserName).ToList();
                    }
                    break;
                // Add more cases as needed, for in-memory sorting
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }

            // Then apply pagination on the sorted list
            var paginatedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = users.Count;

            return new UserDetailsResponseAPIModel { Users = paginatedUsers, TotalCount = totalCount };
        }

    }
}
