namespace mentorship_program_tool.Models.ApiModel
{
    public class GetMeetingsByEmployeeIdApiModel
    {
        public int MeetingID { get; set; }
        public int ProgramID { get; set; }
        public string Title { get; set; }
        public string MenteeFirstName { get; set; }
        public string MentorFirstName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int MeetingStatus { get; set; }
    }
}
