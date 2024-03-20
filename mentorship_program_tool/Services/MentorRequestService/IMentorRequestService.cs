using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public interface IMentorRequestService
    {
        Task CreateRequest(MentorRequestAPIModel mentorRequestAPIModel);
        Task<MentorRequestResponseAPIModel> GetPendingRequests(int status, int pageNumber, int pageSize);
    }
}
