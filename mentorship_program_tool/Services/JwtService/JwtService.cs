using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using mentorship_program_tool.Models.GraphModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace mentorship_program_tool.Services.JwtService
{
    public class JwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _key = jwtSettings.Value.Key;
            _issuer = jwtSettings.Value.Issuer;
            _audience = jwtSettings.Value.Audience;
        }

        public string GenerateJwtToken(GraphUserData userData, DateTime azureTokenExpiration, IEnumerable<string> roles, int employeeID)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userData.EmployeeId),
                new Claim(JwtRegisteredClaimNames.Email, userData.UserPrincipalName),
                new Claim(JwtRegisteredClaimNames.Name, userData.GivenName + " "+ userData.SurName),
                new Claim(JwtRegisteredClaimNames.NameId,employeeID.ToString() ),
            };

            // Add roles as claims
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: azureTokenExpiration.AddMinutes(-2), // Subtract 2 minutes from the Azure token's expiration
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
