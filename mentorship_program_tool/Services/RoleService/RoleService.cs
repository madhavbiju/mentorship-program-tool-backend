using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace mentorship_program_tool.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Role> GetRoles()
        {

            var roles = _unitOfWork.Role.GetAll();

            return roles;
        }

        public Role GetRoleById(int id)
        {
            var roles = _unitOfWork.Role.GetById(id);


            return roles;
        }

        public void CreateRole(Role rolemodel)
        {
            _unitOfWork.Role.Add(rolemodel);
            _unitOfWork.Complete();
        }

        public void UpdateRole(int id, Role rolemodel)
        {
            var existingRole = _unitOfWork.Role.GetById(id);

            if (existingRole == null)
            {
                // Handle not found scenario
                return;
            }

            existingRole.RoleName = rolemodel.RoleName;


            _unitOfWork.Complete();
        }

        public void DeleteRole(int id)
        {
            var roles = _unitOfWork.Role.GetById(id);

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

