﻿using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.AdminApprovalRequestService
{
    public interface IAdminApprovalRequestService
    {
        void UpdateRequest(int id, AdminApprovalAPIModel adminApprovalApiModel);
    }
}
