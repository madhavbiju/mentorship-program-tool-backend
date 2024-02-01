using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MenteeTaskSubmissionService
{
    public class MenteeTaskSubmissionService : IMenteeTaskSubmissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenteeTaskSubmissionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SubmitTask(int ID, MenteeTaskSubmissionAPIModel menteeTaskSubmissionAPIModel)
        {
            var existingTask = _unitOfWork.menteeTaskSubmissionRepository.GetById(ID);

            if (existingTask == null)
            {
                return;
            }

            existingTask.FilePath = menteeTaskSubmissionAPIModel.FilePath;
            existingTask.SubmissionTime = DateTime.Now;

            _unitOfWork.Complete();
        }
    }
}
