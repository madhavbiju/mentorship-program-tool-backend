using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services
{
    public interface IGetAllActiveMentorService
    {
        IEnumerable<GetAllActiveMentorAPIModel> GetAllActiveMentors();
    }
}
