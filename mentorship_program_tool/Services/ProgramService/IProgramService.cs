using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.ProgramService
{
    public interface IProgramService
    {
        Task<ProgramDetailsResponseAPIModel> GetProgram(int status, int pageNumber, int pageSize);
        GetProgramsofMentorResponseApiModel GetProgramsofMentor(int id, int pageNumber, int pageSize);

        GetPairByProgramIdAPIModel GetPairDetailsById(int id);
        Task<Models.EntityModel.Program> GetProgramById(int id);
        Task CreateProgram(Models.EntityModel.Program program);
        void UpdateProgram(int id, ProgramAPIModel program);
        void DeleteProgram(int id);
    }
}
