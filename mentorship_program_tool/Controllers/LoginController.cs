using Azure.Core;
using mentorship_program_tool.Data;
using mentorship_program_tool.Middleware;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Models.GraphModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Services.GraphAPIService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mentorship_program_tool.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly GraphApiService _graphApiService;
        private readonly AppDbContext _dbContext;
        private readonly JwtService _jwtService;

        public LoginController(GraphApiService graphApiService, AppDbContext dbContext, JwtService jwtService)
        {
            _graphApiService = graphApiService;
            _dbContext = dbContext;
            _jwtService = jwtService;
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
                            // Fetch roles based on the employee ID
                            var employeeRoleMappings = await _dbContext.EmployeeRoleMappings
                                                .Where(erm => erm.EmployeeID == existingEmployee.EmployeeID)
                                                .ToListAsync();

                            List<string> roles = new List<string>();

                            if (employeeRoleMappings.Any())
                            {
                                var roleEntities = await _dbContext.Roles
                                    .Where(r => employeeRoleMappings.Select(erm => erm.RoleID).Contains(r.RoleID))
                                    .ToListAsync();

                                roles = roleEntities.Select(role => role.RoleName).ToList();
                            }

                            // Generate JWT with roles for existing user
                            var token = _jwtService.GenerateJwtToken(userGraphData, userInfo.ExpiryTime, roles); // Adjust token expiration as needed
                            return Ok(new { token = token, message = "Existing user", roles = roles });
                        }
                        else
                        {
                            // Logic for new user creation, simplified for brevity
                            // Note: Adjust according to your logic for handling new user roles, if applicable

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

                            // Assuming new users do not immediately have roles, generate JWT without roles
                            // For roles upon creation, adjust logic accordingly
                            var token = _jwtService.GenerateJwtToken(userGraphData, userInfo.ExpiryTime, null); // Adjust token expiration as needed
                            return Ok(new { token = token, message = "New user created" });
                        }
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
