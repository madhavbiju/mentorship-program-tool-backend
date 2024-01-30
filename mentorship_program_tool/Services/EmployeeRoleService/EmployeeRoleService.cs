using mentorship_program_tool.Models.EntityModel;
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

        public IEnumerable<EmployeeRoleMappingModel> GetEmployeeRoles()
        {

            var employeeRoles = _unitOfWork.employeeRoleRepository.GetAll();

            return employeeRoles;
        }

        public EmployeeRoleMappingModel GetEmployeeRoleById(int id)
        {
            var employeeRoles = _unitOfWork.employeeRoleRepository.GetById(id);


            return employeeRoles;
        }

        public void CreateEmployeeRole(EmployeeRoleMappingModel rolemodel)
        {

            _unitOfWork.employeeRoleRepository.Add(rolemodel);
            _unitOfWork.Complete();
        }

        public void UpdateEmployeeRole(int id, EmployeeRoleMappingModel rolemodel)
        {
            var existingRole = _unitOfWork.employeeRoleRepository.GetById(id);

            if (existingRole == null)
            {
                // Handle not found scenario
                return;
            }

            existingRole.RoleId = rolemodel.RoleId;
            existingRole.ModifiedBy = rolemodel.ModifiedBy;
            existingRole.ModifiedTime = rolemodel.ModifiedTime;


            _unitOfWork.Complete();
        }


    }
}
