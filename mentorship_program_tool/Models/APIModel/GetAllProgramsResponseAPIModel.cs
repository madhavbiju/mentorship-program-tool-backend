namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllProgramsResponseAPIModel
    {
        public IEnumerable<GetAllProgramsAPIModel> Programs { get; set; }
        public int TotalCount { get; set; }
    }
}
