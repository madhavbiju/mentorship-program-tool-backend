using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.EmployeeRoleService
{
    public interface IEmployeeRoleService
    {
        IEnumerable<EmployeeRoleMappingModel> GetEmployeeRoles();
        EmployeeRoleMappingModel GetEmployeeRoleById(int id);
        void CreateEmployeeRole(EmployeeRoleMappingModel rolemodel);
        void UpdateEmployeeRole(int id, EmployeeRoleMappingModel rolemodel);
    }
}
