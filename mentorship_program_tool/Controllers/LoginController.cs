using Azure.Core;
using mentorship_program_tool.Data;
using mentorship_program_tool.Middleware;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.GraphAPIService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly GraphApiService _graphApiService;
        private readonly AppDbContext _dbContext;

        public LoginController(GraphApiService graphApiService, AppDbContext dbContext)
        {
            _graphApiService = graphApiService;
            _dbContext = dbContext;
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser()
        {
            try
            {
                var userInfo = HttpContext.Items["UserInfo"] as TokenDecodingMiddleware.UserInfo;

                if (userInfo != null)
                {
                    var accessToken = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                    var userGraphData = await _graphApiService.GetUserDetailsAsync(accessToken);

                    if (userGraphData != null)
                    {
                        var existingEmployee = await _dbContext.Employees
                            .FirstOrDefaultAsync(e => e.OutlookEmployeeID == userGraphData.EmployeeId);

                        if (existingEmployee != null)
                        {
                            var employeeRoleMappings = await _dbContext.EmployeeRoleMappings
                                .Where(erm => erm.EmployeeID == existingEmployee.EmployeeID)
                                .ToListAsync();

                            if (employeeRoleMappings.Any())
                            {
                                var roles = await _dbContext.Roles
                                    .Where(r => employeeRoleMappings.Select(erm => erm.RoleID).Contains(r.RoleID))
                                    .ToListAsync();

                                return Ok(new
                                {
                                    message = "User exists",
                                    roles = roles.Select(role => new { roleId = role.RoleID, roleName = role.RoleName }).ToList() // Return all roles
                                });
                            }
                            else
                            {
                                return Ok(new { message = "User exists but no roles assigned" });
                            }
                        }

                        var employee = new Employee
                        {
                            OutlookEmployeeID = userGraphData.EmployeeId,
                            FirstName = userGraphData.GivenName,
                            LastName = userGraphData.SurName,
                            EmailId = userGraphData.UserPrincipalName,
                            CreatedDate = DateTime.UtcNow,
                            AccountStatus = "active"
                        };

                        _dbContext.Employees.Add(employee);
                        await _dbContext.SaveChangesAsync();

                        return Ok(new { message = "Waiting for admin's approval" });
                    }
                    else
                    {
                        return StatusCode(500, "Error retrieving user details");
                    }
                }
                else
                {
                    return StatusCode(401, "Unauthorized");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}
