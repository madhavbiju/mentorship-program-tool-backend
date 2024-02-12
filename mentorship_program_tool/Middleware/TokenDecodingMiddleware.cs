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
                    var jsonToken = handler.ReadJwtToken(token); // Use ReadJwtToken for a more specific method

                    // Distinguish between Azure AD and custom tokens based on the presence of "upn" (for Azure AD)
                    var isAzureToken = jsonToken.Claims.Any(c => c.Type == "upn");
                    var isCustomToken = jsonToken.Claims.Any(c => c.Type == "Email"); // Assuming custom tokens have an "Email" claim

                    if (isAzureToken)
                    {
                        // Extract the expiry time from the token
                        var expClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;
                        var expiryTimeUtc = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim)).UtcDateTime;


                        var userInfo = new UserInfo
                        {
                            UserId = jsonToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value,
                            UserName = jsonToken.Claims.FirstOrDefault(c => c.Type == "upn")?.Value,
                            ExpiryTime = expiryTimeUtc // Set the expiry time
                        };
                        context.Items["UserInfo"] = userInfo;
                    }
                    else if (isCustomToken)
                    {
                        // Extract user info from custom token
                        var userInfo = new UserInfo
                        {
                            UserId = jsonToken.Claims.FirstOrDefault(c => c.Type == "sub")?.Value,
                            UserName = jsonToken.Claims.FirstOrDefault(c => c.Type == "Email")?.Value,
                        };
                        context.Items["UserInfo"] = userInfo;
                    }
                    else
                    {
                        // Log or handle the case where the token is neither Azure AD nor a recognized custom token
                        Console.WriteLine("Unrecognized token type.");
                    }

                    Console.WriteLine("TokenDecodingMiddleware invoked and processed.");
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Token Decoding Error: {ex.Message}");
                }
            }

            await _next(context);
        }

        public class UserInfo
        {
            public string UserId { get; set; }
            public string UserName { get; set; }
            public DateTime ExpiryTime { get; set; } // New property for expiry time
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
