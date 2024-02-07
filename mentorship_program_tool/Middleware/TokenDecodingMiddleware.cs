using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace mentorship_program_tool.Middleware
{
    public class TokenDecodingMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenDecodingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

                    // Access token decoding
                    var userInfo = new UserInfo
                    {
                        UserId = jsonToken?.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
                        UserName = jsonToken?.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value,
                        // Add other user information you want to extract
                    };
                    Console.WriteLine("TokenDecodingMiddleware invoked");
                    // Set user info in the request
                    context.Items["UserInfo"] = userInfo;

                    // Id token decoding (assuming roles are in id token)
                    var roles = jsonToken?.Claims.Where(c => c.Type == "roles").Select(c => c.Value).ToList();
                    context.Items["Roles"] = roles;
                    Console.WriteLine("TokenDecodingMiddleware completed");
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine($"Token Decoding Error: {ex.Message}");
                }
            }

            await _next(context);
        }

        // Moved UserInfo class outside the TokenDecodingMiddleware class
        public class UserInfo
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            // Add other properties as needed
        }
    }

    public static class TokenDecodingMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenDecodingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenDecodingMiddleware>();
        }
    }

}
