using mentorship_program_tool.Models.EntityModel;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.RoleService
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetRoles();
        Task<Role> GetRoleById(int id);
        Task CreateRole(Role rolemodel);
        void UpdateRole(int id, Role rolemodel);
        void DeleteRole(int id);
    }
}
