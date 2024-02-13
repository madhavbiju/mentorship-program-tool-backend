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
            else if (role.ToLower() == "assigned")
            {
                // Logic for users with any role assigned
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

                var paginatedAssignedUsers = assignedUsersQuery.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                var totalCountAssigned = assignedUsersQuery.Count();

                return new UserDetailsResponseAPIModel { Users = paginatedAssignedUsers, TotalCount = totalCountAssigned };
            }

            else
            {
                // Find users who have the specified role
                var roleId = _context.Roles.FirstOrDefault(r => r.RoleName.ToLower() == role.ToLower())?.RoleID;
                if (roleId == null)
                {
                    return new UserDetailsResponseAPIModel { Users = Enumerable.Empty<GetUserDetailsAPIModel>(), TotalCount = 0 };
                }

                // Fetching EmployeeIDs of users having the specified role
                var usersWithRole = _context.EmployeeRoleMappings
                    .Where(erm => erm.RoleID == roleId)
                    .Select(erm => erm.EmployeeID)
                    .Distinct();

                // Now fetch all roles for these users
                var employeeDetailsQuery = from e in _context.Employees
                                           where usersWithRole.Contains(e.EmployeeID)
                                           select new
                                           {
                                               e.EmployeeID,
                                               e.FirstName,
                                               e.LastName,
                                               e.AccountStatus,
                                               UserRoles = (from erm in _context.EmployeeRoleMappings
                                                            join r in _context.Roles on erm.RoleID equals r.RoleID
                                                            where erm.EmployeeID == e.EmployeeID
                                                            select r.RoleName).Distinct().ToList()
                                           };

                var userData = employeeDetailsQuery.ToList();

                var userDetails = userData
                    .Select(e => new GetUserDetailsAPIModel
                    {
                        UserID = e.EmployeeID,
                        UserName = $"{e.FirstName} {e.LastName}",
                        UserRoles = e.UserRoles,
                        UserJob = "Lead Tester", // Adjust as necessary
                        UserStatus = e.AccountStatus
                    });

                int totalCount = userDetails.Count();
                var paginatedData = userDetails
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();



                return new UserDetailsResponseAPIModel { Users = paginatedData, TotalCount = totalCount };
            }
        }
    }
}


