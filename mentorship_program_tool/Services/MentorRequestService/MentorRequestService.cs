using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.MentorRequestService
{
    public class MentorRequestService : IMentorRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MentorRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // Mentor adding a request (status ID will be 4 and modified by will be null)
        public void CreateRequest(MentorRequestAPIModel mentorRequestAPIModel)
        {
            var request = MapToProgramExtension(mentorRequestAPIModel);
            _unitOfWork.mentorRequestRepository.Add(request);
            _unitOfWork.Complete();
        }

        private ProgramExtension MapToProgramExtension(MentorRequestAPIModel mentorRequestAPIModel)
        {
            return new ProgramExtension
            {
                ProgramID = mentorRequestAPIModel.ProgramID,
                NewEndDate = mentorRequestAPIModel.NewEndDate,
                Reason = mentorRequestAPIModel.Reason,
                RequestStatusID = 4,
                ModifiedBy = mentorRequestAPIModel.ModifiedBy,
                CreatedTime = DateTime.Now

            };
        }

        // Get all pending requests
        public IEnumerable<ProgramExtension> GetPendingRequests()
        {
            var pendingRequests = _unitOfWork.mentorRequestRepository.GetAll().Where(n => n.RequestStatusID == 4);
            return pendingRequests;
        }
    }
}
