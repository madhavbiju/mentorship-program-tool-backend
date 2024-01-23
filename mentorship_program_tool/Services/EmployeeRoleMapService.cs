using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services
{
    public class EmployeeRoleMapService : IEmployeeRoleMapService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeRoleMapService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeRoleMappingAPI> GetEmployeesWithRole()
        {

            var employees = _unitOfWork.EmployeeRoleMap.GetAll();
            return MapToEmployeeRoleMapAPIList(employees);
        }

        public EmployeeRoleMappingAPI GetEmployeeRolesById(int id)
        {
            var employee = _unitOfWork.EmployeeRoleMap.GetById(id);
            return MapToEmployeeRoleMapAPI(employee);
        }

        public void CreateEmployeeRoleMap(EmployeeRoleMappingAPI employeeDto)
        {
            var employee = MapToEmployee(employeeDto);
            _unitOfWork.EmployeeRoleMap.Add(employee);
            _unitOfWork.Complete();
        }

        public void UpdateEmployeeRoleMap(int id, EmployeeRoleMappingAPI employeeDto)
        {
            var existingEmployee = _unitOfWork.EmployeeRoleMap.GetById(id);

            if (existingEmployee == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on employeeDt
            existingEmployee.employeeid = employeeDto.employeeid;
            existingEmployee.roleid = employeeDto.roleid;
            existingEmployee.modifiedby = employeeDto.createdby;
            existingEmployee.modifiedtime = DateTime.Now;

            _unitOfWork.Complete();
        }

        public void DeleteEmployeeRoleMap(int id)
        {
            var employee = _unitOfWork.EmployeeRoleMap.GetById(id);

            if (employee == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.EmployeeRoleMap.Delete(employee);
            _unitOfWork.Complete();
        }

        private EmployeeRoleMappingAPI MapToEmployeeRoleMapAPI(EmployeeRoleMapModel employee)
        {
            return new EmployeeRoleMappingAPI
            {
                employeerolemappingid = employee.employeerolemappingid,
                employeeid = employee.employeeid,
                roleid = employee.roleid,
                createdby = employee.createdby
            };
        }

        private IEnumerable<EmployeeRoleMappingAPI> MapToEmployeeRoleMapAPIList(IEnumerable<EmployeeRoleMapModel> employees)
        {
            return employees.Select(MapToEmployeeRoleMapAPI);
        }

        private EmployeeRoleMapModel MapToEmployee(EmployeeRoleMappingAPI employeeDto)
        {
            return new EmployeeRoleMapModel
            {
                employeerolemappingid = employeeDto.employeerolemappingid,
                employeeid = employeeDto.employeeid,
                roleid = employeeDto.roleid,
                createdby = employeeDto.createdby,
                createdtime = DateTime.Now,
                modifiedby = null,
                modifiedtime = null
            };
        }
    }
}
