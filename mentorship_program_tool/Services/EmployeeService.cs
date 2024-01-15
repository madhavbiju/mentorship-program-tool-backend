using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<EmployeeModel> GetEmployees()
        {

            var employees = _unitOfWork.Employees.GetAll();
            return MapToEmployeeModelList(employees);
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            return MapToEmployeeModel(employee);
        }

        public void CreateEmployee(EmployeeModel employeeDto)
        {
            var employee = MapToEmployee(employeeDto);
            _unitOfWork.Employees.Add(employee);
            _unitOfWork.Complete();
        }

        public void UpdateEmployee(int id, EmployeeModel employeeDto)
        {
            var existingEmployee = _unitOfWork.Employees.GetById(id);

            if (existingEmployee == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on employeeDto
            existingEmployee.Name = employeeDto.Name;


            _unitOfWork.Complete();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);

            if (employee == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.Employees.Delete(employee);
            _unitOfWork.Complete();
        }

        private EmployeeModel MapToEmployeeModel(EmployeeModel employee)
        {
            return new EmployeeModel
            {
                Id = employee.Id,
                Name = employee.Name,

            };
        }

        private IEnumerable<EmployeeModel> MapToEmployeeModelList(IEnumerable<Employee> employees)
        {
            return employees.Select(MapToEmployeeModel);
        }

        private EmployeeModel MapToEmployee(EmployeeModel employeeDto)
        {
            return new EmployeeModel
            {
                Name = employeeDto.Name,

            };
        }

    }
}
