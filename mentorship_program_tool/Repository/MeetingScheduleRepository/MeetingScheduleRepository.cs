using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository.MeetingScheduleReposixtory;

namespace mentorship_program_tool.Repository.MeetingScheduleRepository
{
    public class MeetingScheduleRepository : Repository<MeetingSchedule>, IMeetingScheduleRepository
    {
        public MeetingScheduleRepository(AppDbContext context) : base(context) { }
    }
}
