using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.PutProgramExtensionService
{
    public interface IProgramDateExtensionService
    {
        Task<bool> UpdateProgramDateAsync(ProgramDateUpdateAPIModel model);
    }
}
