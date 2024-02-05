using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using System.Collections.Generic;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public interface IMentorRequestService
    {
        void CreateRequest(MentorRequestAPIModel mentorRequestAPIModel);
        IEnumerable<ProgramExtension> GetPendingRequests();
    }
}
