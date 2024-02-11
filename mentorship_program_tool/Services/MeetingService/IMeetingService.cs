using System.Collections.Generic;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.MeetingService
{
    public interface IMeetingService
    {
        IEnumerable<MeetingSchedule> GetMeetings();
        IEnumerable<MeetingSchedule> GetMeetingByEmployeeId(int id,int role);
        MeetingSchedule GetMeetingById(int id);
        void CreateMeeting(MeetingSchedule meeting);
        void DeleteMeeting(int id);
    }
}