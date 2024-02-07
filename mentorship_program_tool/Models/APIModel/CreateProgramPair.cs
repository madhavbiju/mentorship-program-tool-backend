namespace mentorship_program_tool.Models.APIModel
{
    public class CreateProgramPair
    {
        public string ProgramName { get; set; }
        public int MentorID { get; set; }
        public int MenteeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
