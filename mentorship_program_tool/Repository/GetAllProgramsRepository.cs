using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Repository
{
   
        public class GetAllProgramsRepository : Repository<GetAllProgramsAPIModel>, IGetAllProgramsRepository
    {
            public GetAllProgramsRepository(AppDbContext context) : base(context) { }
        }
    
}
