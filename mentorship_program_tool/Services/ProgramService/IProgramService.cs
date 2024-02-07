using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.ProgramService
{
    public interface IProgramService
    {
        ProgramDetailsResponseAPIModel GetProgram(int status, int pageNumber, int pageSize);
        Models.EntityModel.Program GetProgramById(int id);
        void CreateProgram(Models.EntityModel.Program program);
        void UpdateProgram(int id, Models.EntityModel.Program program);
        void DeleteProgram(int id);
    }
}
