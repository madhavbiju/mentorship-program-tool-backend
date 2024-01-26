using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Repository.GetActiveTasksRepository;

namespace mentorship_program_tool.Repository.GetTasksByEmployeeIdRepository
{
    public class GetTasksbyEmployeeIdRepository : Repository<GetTasksByEmployeeIdAPIModel>, IGetTasksbyEmployeeIdRepository
    {
        public GetTasksbyEmployeeIdRepository(AppDbContext context) : base(context) { }

    }
}
