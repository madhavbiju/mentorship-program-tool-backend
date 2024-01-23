﻿using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services
{
    public class AdminApprovalRequestService : IAdminApprovalRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminApprovalRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void UpdateRequest(int id, AdminApprovalAPIModel adminapprovalapimodel)
        {
            var existingRequest = _unitOfWork.adminApprovalRequestRepository.GetById(id);

            if (existingRequest == null)
            {
                return;
            }

            // Update properties based on employeeDto
            existingRequest.requeststatusid = adminapprovalapimodel.requeststatusid;
            existingRequest.modifiedby = adminapprovalapimodel.modifiedby;


            _unitOfWork.Complete();
        }
    }
}
