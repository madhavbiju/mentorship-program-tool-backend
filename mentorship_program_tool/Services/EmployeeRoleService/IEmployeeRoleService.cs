using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.EmployeeRoleService
{
    public interface IEmployeeRoleService
    {
        /*        IEnumerable<EmployeeRoleMapping> GetEmployeeRoles();
                EmployeeRoleMapping GetEmployeeRoleById(int id);
                void CreateEmployeeRole(EmployeeRoleMapping rolemodel);
                void UpdateEmployeeRole(int id, EmployeeRoleMapping rolemodel);*/
        public void UpdateEmployeeRoles(AssignRolesToEmployeeModel model, int adminUserId);
    }
}
