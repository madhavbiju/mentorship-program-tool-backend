using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services.MenteeTaskSubmissionService
{
    public interface IMenteeTaskSubmissionService
    {
        //updation
        void SubmitTask(int id, MenteeTaskSubmissionAPIModel menteetasksubmissionapimodel);
    }
}
