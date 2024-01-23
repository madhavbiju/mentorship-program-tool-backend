using mentorship_program_tool.Models.EntityModel;
using NuGet.Protocol.Plugins;
using System.Security.Claims;

namespace mentorship_program_tool.Services
{
    public interface ILoginService
    {
        public string Login(LoginModel login, List<Claim> authClaim);

    }
}
