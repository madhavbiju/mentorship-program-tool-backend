using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleModel> GetRoles();
        RoleModel GetRoleById(int id);
        void CreateRole(RoleModel rolemodel);
        void UpdateRole(int id, RoleModel rolemodel);
        void DeleteRole(int id);
    }
}
