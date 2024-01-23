using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Repository.GetAllMenteeDetailsByIdRepository
{
    public class GetMenteeDetailsByIdRepository: Repository<GetMenteeDetailsByIdAPIModel>, IGetMenteeDetailsByIdRepository
    {
        public GetMenteeDetailsByIdRepository(AppDbContext context) : base(context) { }

    }
}
