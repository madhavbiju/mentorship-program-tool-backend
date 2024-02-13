/*using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.EmployeeRoleService
{
    public class EmployeeRoleService : IEmployeeRoleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRoleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeRoleMapping> GetEmployeeRoles()
        {

            var employeeRoles = _unitOfWork.employeeRoleRepository.GetAll();

            return employeeRoles;
        }

        public EmployeeRoleMapping GetEmployeeRoleById(int id)
        {
            var employeeRoles = _unitOfWork.employeeRoleRepository.GetById(id);


            return employeeRoles;
        }

        public void CreateEmployeeRole(EmployeeRoleMapping rolemodel)
        {

            _unitOfWork.employeeRoleRepository.Add(rolemodel);
            _unitOfWork.Complete();
        }

        public void UpdateEmployeeRole(int id, EmployeeRoleMapping rolemodel)
        {
            var existingRole = _unitOfWork.employeeRoleRepository.GetById(id);

            if (existingRole == null)
            {
                // Handle not found scenario
                return;
            }

            existingRole.RoleID = rolemodel.RoleID;
            existingRole.ModifiedBy = rolemodel.ModifiedBy;
            existingRole.ModifiedTime = rolemodel.ModifiedTime;


            _unitOfWork.Complete();
        }


    }
}
*/

using mentorship_program_tool.Data;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.Services.EmployeeRoleService;
using System;
using System.Linq;

namespace mentorship_program_tool.Services.EmployeeRoleService
{
    public class EmployeeRoleService: IEmployeeRoleService
    {
        private readonly AppDbContext _context;

        public EmployeeRoleService(AppDbContext context)
        {
            _context = context;
        }

        public void UpdateEmployeeRoles(AssignRolesToEmployeeModel model, int adminUserId)
        {
            var currentRoles = _context.EmployeeRoleMappings
                .Where(x => x.EmployeeID == model.EmployeeID)
                .ToList();

            int mentorRoleId = GetRoleIdByName("mentor");
            int menteeRoleId = GetRoleIdByName("mentee");

            // Assign or remove the Mentor role
            if (model.IsMentor != currentRoles.Any(x => x.RoleID == mentorRoleId))
            {
                if (model.IsMentor)
                {
                    AssignRole(model.EmployeeID, mentorRoleId, adminUserId);
                }
                else
                {
                    RemoveRole(model.EmployeeID, mentorRoleId);
                }
            }

            // Assign or remove the Mentee role
            if (model.IsMentee != currentRoles.Any(x => x.RoleID == menteeRoleId))
            {
                if (model.IsMentee)
                {
                    AssignRole(model.EmployeeID, menteeRoleId, adminUserId);
                }
                else
                {
                    RemoveRole(model.EmployeeID, menteeRoleId);
                }
            }

            _context.SaveChanges();
        }

        private void AssignRole(int employeeId, int roleId, int createdBy)
        {
            var newRoleMapping = new EmployeeRoleMapping
            {
                EmployeeID = employeeId,
                RoleID = roleId,
                CreatedBy = createdBy,
                CreatedTime = DateTime.Now
            };
            _context.EmployeeRoleMappings.Add(newRoleMapping);
        }

        private void RemoveRole(int employeeId, int roleId)
        {
            var roleMapping = _context.EmployeeRoleMappings
                .FirstOrDefault(x => x.EmployeeID == employeeId && x.RoleID == roleId);
            if (roleMapping != null)
            {
                _context.EmployeeRoleMappings.Remove(roleMapping);
            }
        }

        private int GetRoleIdByName(string roleName)
        {
            roleName = roleName.ToLower();
            var role = _context.Roles
                .FirstOrDefault(r => r.RoleName.ToLower() == roleName);

            return role?.RoleID ?? 0; // Assuming 0 is an invalid RoleID, indicating not found
        }
    }
}
