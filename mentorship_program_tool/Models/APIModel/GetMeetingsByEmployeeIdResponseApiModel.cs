namespace mentorship_program_tool.Models.ApiModel
{
    public class GetMeetingsByEmployeeIdResponseApiModel
    {
        public IEnumerable<GetMeetingsByEmployeeIdApiModel> Meetings { get; set; }
        public int TotalCount { get; set; }
    }
}
