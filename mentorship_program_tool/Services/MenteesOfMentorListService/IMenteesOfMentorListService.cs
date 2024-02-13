using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.MenteesOfMentorListService
{
    public interface IMenteesOfMentorListService
    {
        List<MenteesOfMentorListAPIModel> GetAllMenteesById(int id);
    }
}
