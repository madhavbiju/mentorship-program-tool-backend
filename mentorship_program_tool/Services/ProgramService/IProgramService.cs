using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.ProgramService
{
    public interface IProgramService
    {
        IEnumerable<ProgramModel> GetProgram();
        ProgramModel GetProgramById(int id);
        void CreateProgram(ProgramModel program);
        void DeleteProgram(int id);
    }
}
