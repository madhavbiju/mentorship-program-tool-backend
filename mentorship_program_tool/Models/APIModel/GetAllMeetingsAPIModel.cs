using System.Diagnostics.Metrics;

namespace mentorship_program_tool.Models.ApiModel
{
    public class GetAllMeetingsAPIModel
    {
        public string menteeName { get; set; }
        public int meetingID { get; set; }
        public string meetingName { get; set; }
        public DateTime scheduledDate { get; set; }

        public DateTime scheduledTime { get; set; }

        public string Status { get; set; }

            
    }
}
