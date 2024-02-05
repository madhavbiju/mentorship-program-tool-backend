using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Repository.GetActiveTasksRepository
{
    public class GetTasksByProgramIdRepository : Repository<GetTasksByProgramIdAPIModel>, IGetTasksByProgramIdRepository
    {
        public GetTasksByProgramIdRepository(AppDbContext context) : base(context) { }
    }
}
