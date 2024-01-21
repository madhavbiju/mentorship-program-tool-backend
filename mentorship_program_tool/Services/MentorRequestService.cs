using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Repository;
using mentorship_program_tool.UnitOfWork;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Drawing;

namespace mentorship_program_tool.Services
{
    public class MentorRequestService : IMentorRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MentorRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //Mentoring adding a request(status id will be 2 and modifiedby will be null)
        public void CreateRequest(MentorRequestAPIModel mentorrequestapimodel)
        {
            var request = MapToProgramExtension(mentorrequestapimodel);
            _unitOfWork.mentorRequestRepository.Add(request);
            _unitOfWork.Complete();
        }
        private MentorRequestModel MapToProgramExtension(MentorRequestAPIModel mentorrequestapimodel)
        {
            return new MentorRequestModel
            {
                programid = mentorrequestapimodel.programid,
                newenddate = mentorrequestapimodel.newenddate,
                reason = mentorrequestapimodel.reason,
                requeststatusid = 2,
                modifiedby = null,
                createdtime = DateTime.Now

            };
        }

        //getall pending request
        public IEnumerable<MentorRequestModel> GetPendingRequest()
        {
            var pendingRequest = _unitOfWork.mentorRequestRepository.GetAll().Where(n => n.requeststatusid == 2);
            return pendingRequest;
        }


    }
}
