using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public interface IMentorRequestService
    {
        void CreateRequest(MentorRequestAPIModel mentorrequestapimodel);
        IEnumerable<MentorRequestModel> GetPendingRequest();
    }
}
