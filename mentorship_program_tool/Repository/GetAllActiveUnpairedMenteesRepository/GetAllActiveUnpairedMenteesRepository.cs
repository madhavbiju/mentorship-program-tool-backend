using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Repository
{
   
        public class GetAllActiveUnpairedMenteesRepository : Repository<GetAllActiveUnpairedMenteesAPIModel>, IGetAllActiveUnpairedMenteesRepository
    {
            public GetAllActiveUnpairedMenteesRepository(AppDbContext context) : base(context) { }
        }
    
}
