using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.GetAllMenteesOfMentorService
{
    public interface IGetAllMenteesOfMentorService
    {
        List<GetAllMenteesOfMentorAPIModel> GetAllMenteesById(int id);
    }
}
