using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetUserDetailsService
{
    public interface IGetUserDetailsService
    {
        public UserDetailsResponseAPIModel GetUserDetails(string role, int pageNumber, int pageSize, string sortParameter, string sortType);
    }
}
