using mentorship_program_tool.Models.APIModel;

namespace mentorship_program_tool.Services
{
    public interface IEmployeeRoleMapService
    {
        IEnumerable<EmployeeRoleMappingAPI> GetEmployeesWithRole();
        EmployeeRoleMappingAPI GetEmployeeRolesById(int id);
        void CreateEmployeeRoleMap(EmployeeRoleMappingAPI employeeRoleMapDto);
        void UpdateEmployeeRoleMap(int id, EmployeeRoleMappingAPI employeeRoleMapDto);
        void DeleteEmployeeRoleMap(int id);
    }
}
