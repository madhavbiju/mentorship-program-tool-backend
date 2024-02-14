using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.GetProgramExtensionService
{
    public interface IProgramExtensionService
    {
        Task<ProgramExtensionRequestResponseAPIModel> GetAllProgramExtensionRequestsAsync(int status, int pageNumber, int pageSize);
    }
}
