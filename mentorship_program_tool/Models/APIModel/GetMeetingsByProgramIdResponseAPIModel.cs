namespace mentorship_program_tool.Models.ApiModel
{
    public class GetMeetingsByProgramIdResponseAPIModel
    {
        public IEnumerable<GetMeetingsByProgramIdAPIModel> Meetings { get; set; }
        public int TotalCount { get; set; }
    }
}
