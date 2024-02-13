namespace mentorship_program_tool.Models.ApiModel
{
    public class GetAllMeetingsResponseAPIModel
    {
        public IEnumerable<GetAllMeetingsAPIModel> Meetings { get; set; }
        public int TotalCount { get; set; }
    }
}
