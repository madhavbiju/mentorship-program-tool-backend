using System.Collections.Generic;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.MeetingService
{
    public interface IMeetingService
    {
        Task<IEnumerable<MeetingSchedule>> GetMeetings();
        GetAllMeetingsResponseAPIModel GetAllMeetings(int pageNumber, string sort);
        GetMeetingsByProgramIdResponseAPIModel GetMeetingsByProgramId(int ID, int page, string? sortBy);
        GetMeetingsByEmployeeIdResponseApiModel GetMeetingsByEmployeeId(int ID, int page, string? sortBy);

        IEnumerable<MeetingSchedule> GetMeetingByEmployeeId(int id, int role);
        IEnumerable<MeetingSchedule> GetSoonMeetingByEmployeeId(int id);
        Task<MeetingSchedule> GetMeetingById(int id);
        Task CreateMeeting(MeetingSchedule meeting);
        void DeleteMeeting(int id);
    }
}