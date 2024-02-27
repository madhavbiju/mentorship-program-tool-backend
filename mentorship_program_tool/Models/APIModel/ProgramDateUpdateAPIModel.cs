namespace mentorship_program_tool.Models.APIModel
{
    public class ProgramDateUpdateAPIModel
    {
        public int ProgramID { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedTime { get; set; }
        public DateTime EndDate { get; set; }
    }
}
