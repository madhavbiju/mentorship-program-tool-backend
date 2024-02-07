namespace mentorship_program_tool.Models.APIModel
{
    public class GetUserDetailsAPIModel
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public string UserJob { get; set; } = "Lead Tester";
        public string UserStatus { get; set; }
    }
}
