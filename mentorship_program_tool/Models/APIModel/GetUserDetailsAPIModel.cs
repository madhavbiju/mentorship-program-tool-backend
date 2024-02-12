namespace mentorship_program_tool.Models.APIModel
{
    public class GetUserDetailsAPIModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public List<string> UserRoles { get; set; } // Changed to accommodate multiple roles
        public string UserJob { get; set; }
        public string UserStatus { get; set; }
    }

}
