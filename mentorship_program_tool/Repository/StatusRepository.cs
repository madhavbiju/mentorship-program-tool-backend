using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository
{
    public class StatusRepository : Repository<StatusModel>, IStatusRepository
    {
        public StatusRepository(AppDbContext context) : base(context) { }
    }
}
