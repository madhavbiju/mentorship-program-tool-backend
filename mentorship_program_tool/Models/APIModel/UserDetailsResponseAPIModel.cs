namespace mentorship_program_tool.Models.APIModel
{
    public class UserDetailsResponseAPIModel
    {
        public IEnumerable<GetUserDetailsAPIModel> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
