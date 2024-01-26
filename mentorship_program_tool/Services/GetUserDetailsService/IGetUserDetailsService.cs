using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetUserDetailsService
{
    public interface IGetUserDetailsService
    {
        IEnumerable<GetUserDetailsAPIModel> GetUserDetails(string role);
    }
}
