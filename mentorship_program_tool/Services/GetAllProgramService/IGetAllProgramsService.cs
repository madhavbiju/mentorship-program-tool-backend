using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.GetAllProgramService
{
    public interface IGetAllProgramsService
    {
        GetAllProgramsResponseAPIModel GetAllPrograms( int page);

    }
}
