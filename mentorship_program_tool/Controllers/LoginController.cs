using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services;
using mentorship_program_tool.Data;

using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

using System.Text;

namespace mentorship_program_tool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {


        private readonly AppDbContext _context;
        private readonly ILoginService _service;

        public LoginController(AppDbContext context, ILoginService service)
        {
            _context = context;
            _service = service;
        }


        [HttpPost("/login")]
        public ActionResult<LoginModel> Login(LoginModel login)
        {
            //usrnm pass corct statil ano adiche
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //usernme pass chckng
            var employee = _context.register.FirstOrDefault(e => e.email == login.email);
            var employeelogin = _context.login.FirstOrDefault(e => e.email == login.email);
            var employeeRoleMapping = _context.employeerolemapping.FirstOrDefault(e => e.employeeid == employeelogin.employeeid);

            if (employee == null || !VerifyPassword(login.password, employee.password))
            {
                return Unauthorized("Invalid username or password");
            }

            var authClaims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, $"{employee.firstname} {employee.lastname}")
    };

            if (employeeRoleMapping != null)
            {
                // Retrieve all roles based on the employee ID
                var roles = _context.employeerolemapping
                    .Where(r => r.employeeid == employeeRoleMapping.employeeid)
                    .Join(_context.role, erm => erm.roleid, r => r.roleid, (erm, r) => r.roles)
                    .ToList();

                if (roles.Any())
                {
                    // Add all roles to authClaims
                    foreach (var role in roles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }
                }
            }

            var token = _service.Login(login, authClaims);

            return Ok(token);
        }


        //chckng hashed value of entrng pass equals to orgnl passwrd(orgnl pass is already hashed)::return true or false
        private bool VerifyPassword(string enteredpass, string originalpass)
        {
            return originalpass == enteredpass;
        }


        //hashing

    }
}
