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

        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize, string sortParameter, string sortType, string status = "all")
        {
            switch (role.ToLower())
            {
                case "all":
                    return GetAllUsers(pageNumber, pageSize, sortParameter, sortType, status);
                case "unassigned":
                    return GetUnassignedUsers(pageNumber, pageSize, sortParameter, sortType, status);
                case "assigned":
                    return GetAssignedUsers(pageNumber, pageSize, sortParameter, sortType, status);
                default:
                    return GetUsersBySpecificRole(role, pageNumber, pageSize, sortParameter, sortType, status);
            }
        }

        private UserDetailsResponseAPIModel GetAllUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status)
        {
            var query = GetBaseUserQuery(status);
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUnassignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status)
        {
            var query = GetBaseUserQuery(status).Where(e => !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetAssignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status)
        {
            var query = GetBaseUserQuery(status).Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUsersBySpecificRole(string role, int pageNumber, int pageSize, string sortParameter, string sortType, string status)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            var query = GetBaseUserQuery(status).Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID && erm.RoleID == roleId));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private IQueryable<GetUserDetailsAPIModel> GetBaseUserQuery(string status = "all")
        {
            var query = from e in _context.Employees
                        select new GetUserDetailsAPIModel
                        {
                            UserID = e.EmployeeID,
                            UserName = $"{e.FirstName} {e.LastName}",
                            UserRoles = (from erm in _context.EmployeeRoleMappings
                                         join r in _context.Roles on erm.RoleID equals r.RoleID
                                         where erm.EmployeeID == e.EmployeeID
                                         select r.RoleName).Distinct().ToList(),
                            UserJob = "Lead Tester",
                            UserStatus = e.AccountStatus
                        };

            if (status.ToLower() == "active")
            {
                query = query.Where(e => e.UserStatus == "Active");
            }
            else if (status.ToLower() == "inactive")
            {
                query = query.Where(e => e.UserStatus == "Inactive");
            }

            return query;
        }

        private UserDetailsResponseAPIModel PaginateAndSortUsers(IQueryable<GetUserDetailsAPIModel> query, int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var users = query.ToList();

            bool isAscending = sortType.Equals("Asc", StringComparison.OrdinalIgnoreCase);
            switch (sortParameter.ToLower())
            {
                case "username":
                    users = isAscending ? users.OrderBy(u => u.UserName).ToList() : users.OrderByDescending(u => u.UserName).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }

            var paginatedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = users.Count;

            return new UserDetailsResponseAPIModel { Users = paginatedUsers, TotalCount = totalCount };
        }
    }
}

*/


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

        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize, string sortParameter, string sortType, string status = "all", string searchQuery = "")
        {
            switch (role.ToLower())
            {
                case "all":
                    return GetAllUsers(pageNumber, pageSize, sortParameter, sortType, status, searchQuery);
                case "unassigned":
                    return GetUnassignedUsers(pageNumber, pageSize, sortParameter, sortType, status, searchQuery);
                case "assigned":
                    return GetAssignedUsers(pageNumber, pageSize, sortParameter, sortType, status, searchQuery);
                default:
                    return GetUsersBySpecificRole(role, pageNumber, pageSize, sortParameter, sortType, status, searchQuery);
            }
        }

        private UserDetailsResponseAPIModel GetAllUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status, string searchQuery)
        {
            var query = GetBaseUserQuery(status, searchQuery);
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUnassignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status, string searchQuery)
        {
            var query = GetBaseUserQuery(status, searchQuery).Where(e => !_context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetAssignedUsers(int pageNumber, int pageSize, string sortParameter, string sortType, string status, string searchQuery)
        {
            var query = GetBaseUserQuery(status, searchQuery).Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private UserDetailsResponseAPIModel GetUsersBySpecificRole(string role, int pageNumber, int pageSize, string sortParameter, string sortType, string status, string searchQuery)
        {
            var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
            if (roleId == null)
            {
                return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
            }

            var query = GetBaseUserQuery(status, searchQuery).Where(e => _context.EmployeeRoleMappings.Any(erm => erm.EmployeeID == e.UserID && erm.RoleID == roleId));
            return PaginateAndSortUsers(query, pageNumber, pageSize, sortParameter, sortType);
        }

        private IQueryable<GetUserDetailsAPIModel> GetBaseUserQuery(string status = "all", string searchQuery = "")
        {
            var query = from e in _context.Employees
                        where string.IsNullOrEmpty(searchQuery) || (e.FirstName + " " + e.LastName).ToLower().Contains(searchQuery.ToLower())
                        select new GetUserDetailsAPIModel
                        {
                            UserID = e.EmployeeID,
                            UserName = $"{e.FirstName} {e.LastName}",
                            UserRoles = (from erm in _context.EmployeeRoleMappings
                                         join r in _context.Roles on erm.RoleID equals r.RoleID
                                         where erm.EmployeeID == e.EmployeeID
                                         select r.RoleName).Distinct().ToList(),
                            UserJob = "Lead Tester",
                            UserStatus = e.AccountStatus
                        };

            if (status.ToLower() == "active")
            {
                query = query.Where(e => e.UserStatus == "Active");
            }
            else if (status.ToLower() == "inactive")
            {
                query = query.Where(e => e.UserStatus == "Inactive");
            }

            return query;
        }

        private UserDetailsResponseAPIModel PaginateAndSortUsers(IQueryable<GetUserDetailsAPIModel> query, int pageNumber, int pageSize, string sortParameter, string sortType)
        {
            var users = query.ToList();

            bool isAscending = sortType.Equals("Asc", StringComparison.OrdinalIgnoreCase);
            switch (sortParameter.ToLower())
            {
                case "username":
                    users = isAscending ? users.OrderBy(u => u.UserName).ToList() : users.OrderByDescending(u => u.UserName).ToList();
                    break;
                default:
                    throw new ArgumentException("Invalid sort parameter");
            }

            var paginatedUsers = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var totalCount = users.Count;

            return new UserDetailsResponseAPIModel { Users = paginatedUsers, TotalCount = totalCount };
        }
    }
}
