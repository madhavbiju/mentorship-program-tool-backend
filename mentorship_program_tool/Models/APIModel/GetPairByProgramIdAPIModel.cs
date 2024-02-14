namespace mentorship_program_tool.Models.ApiModel
{
    public class GetPairByProgramIdAPIModel
    {
        public string ProgramName { get; set; }
        public string MentorName { get; set; }
        public string MenteeName { get; set; }
        public string ProgramStatus { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
