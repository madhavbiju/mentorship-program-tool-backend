using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class MeetingSchedule
    {

    [Key]
    public int MeetingID { get; set; }

    public int ProgramID { get; set; }

    public DateTime ScheduleDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
        public int MeetingStatus { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedTime { get; set; }

    public DateTime? ModifiedTime { get; set; }

    public String Agenda { get; set; }

    public String Title { get; set; }

    public int? MeetingModeID { get; set; }

    public String? Location { get; set; }
}
}
