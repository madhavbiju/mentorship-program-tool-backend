using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetAllMenteesListService
{
    public interface IGetAllMenteesListService
    {
        IEnumerable<GetAllMenteesListAPIModel> GetAllMenteesList();

    }
}
