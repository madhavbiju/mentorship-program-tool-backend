using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Role>> GetRoles()
        {

            var roles = await _unitOfWork.Role.GetAll();

            return roles;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var roles = await _unitOfWork.Role.GetById(id);


            return roles;
        }

        public async Task CreateRole(Role rolemodel)
        {
            await _unitOfWork.Role.Add(rolemodel);
            _unitOfWork.Complete();
        }

        public async void UpdateRole(int id, Role rolemodel)
        {
            var existingRole = await _unitOfWork.Role.GetById(id);

            if (existingRole == null)
            {
                // Handle not found scenario
                return;
            }

            existingRole.RoleName = rolemodel.RoleName;


            _unitOfWork.Complete();
        }

        public async void DeleteRole(int id)
        {
            var roles = await _unitOfWork.Role.GetById(id);

            if (roles == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.Role.Delete(roles);
            _unitOfWork.Complete();
        }
    }
}

