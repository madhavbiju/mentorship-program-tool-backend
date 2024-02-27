using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.MentorsOfMenteesListService
{
    public interface IMentorsOfMenteesListService
    {
        List<MentorsOfMenteesListAPImodel> GetAllMentorsById(int id);
    }
}
