using System.Collections.Generic;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.MeetingService
{
    public interface IMeetingService
    {
        IEnumerable<MeetingSchedule> GetMeetings();
        GetAllMeetingsResponseAPIModel GetAllMeetings(int pageNumber, string sort);
        GetMeetingsByProgramIdResponseAPIModel GetMeetingsByProgramId(int ID,  int page, string? sortBy);

        IEnumerable<MeetingSchedule> GetMeetingByEmployeeId(int id, int role);
        IEnumerable<MeetingSchedule> GetSoonMeetingByEmployeeId(int id);
        MeetingSchedule GetMeetingById(int id);
        void CreateMeeting(MeetingSchedule meeting);
        void DeleteMeeting(int id);
    }
}