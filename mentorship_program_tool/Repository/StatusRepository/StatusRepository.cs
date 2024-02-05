using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.StatusRepository
{
    public class StatusRepository : Repository<Status>, IStatusRepository
    {
        public StatusRepository(AppDbContext context) : base(context) { }
    }
}
