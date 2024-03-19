namespace mentorship_program_tool.Models.APIModel
{
    public class ProgramExtensionRequestAPIModel
    {
        public int ProgramExtensionID { get; set; }
        public int ProgramID { get; set; }
        public int MenteeID { get; set; }
        public int MentorID { get; set; }
        public string ProgramName { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Reason { get; set; }
        public string MenteeName { get; set; }
        public string MentorName { get; set; }
        public DateTime CurrentEndDate { get; set; }
        public int? RequestStatusID { get; set; }
    }

}
