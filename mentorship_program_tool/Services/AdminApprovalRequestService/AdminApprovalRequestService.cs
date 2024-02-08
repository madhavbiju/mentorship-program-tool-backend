using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.AdminApprovalRequestService
{
    public class AdminApprovalRequestService : IAdminApprovalRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminApprovalRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Admin updating request
        public void UpdateRequest(int id, AdminApprovalAPIModel adminApprovalApiModel)
        {
            var existingRequest = _unitOfWork.adminApprovalRequestRepository.GetById(id);

            if (existingRequest == null)
            {
                return;
            }

            // Update properties based on admin api model
            existingRequest.RequestStatusID = adminApprovalApiModel.RequestStatusID;
            existingRequest.ModifiedBy = adminApprovalApiModel.ModifiedBy;

            _unitOfWork.Complete();
        }
    }
}
