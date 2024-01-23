using mentorship_program_tool.Models.EntityModel;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mentorship_program_tool.Services
{
    public class LoginService : ILoginService
    {

        private IConfiguration _config;    //to access appsettngs.json
        public LoginService(IConfiguration config)
        {
            _config = config;
        }

        public string Login(LoginModel login, List<Claim> authClaim)
        {
            //token generatn

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var SecurityToken = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], authClaim, expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(SecurityToken);


            return token;
        }
    }
}
