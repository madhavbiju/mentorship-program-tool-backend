using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.ProgramRepository
{
    public class ProgramRepository : Repository<Models.EntityModel.Program>, IProgramRepository
    {
        public ProgramRepository(AppDbContext context) : base(context) { }
    }
}
