namespace mentorship_program_tool.Models.APIModel
{
    public class ProgramExtensionRequestResponseAPIModel
    {
        public IEnumerable<ProgramExtensionRequestAPIModel> Requests { get; set; }
        public int TotalCount { get; set; }
    }

}
