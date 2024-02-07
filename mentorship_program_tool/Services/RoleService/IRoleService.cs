using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.RoleService
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
        Role GetRoleById(int id);
        void CreateRole(Role rolemodel);
        void UpdateRole(int id, Role rolemodel);
        void DeleteRole(int id);
    }
}
