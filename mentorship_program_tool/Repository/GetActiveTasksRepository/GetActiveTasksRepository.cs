using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Repository.GetActiveTasksRepository
{
    public class GetActiveTasksRepository : Repository<GetActiveTasksAPIModel>, IGetActiveTasksRepository
    {
        public GetActiveTasksRepository(AppDbContext context) : base(context) { }
    }
}
