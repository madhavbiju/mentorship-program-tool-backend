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
 
        public IEnumerable<EmployeeModel> GetEmployee()
        {
 
            var employee = _unitOfWork.Employees.GetAll();
            return (employee);
        }
 
        public EmployeeModel GetEmployeeById(int id)
        {
            var employee = _unitOfWork.Employees.GetById(id);
            return (employee);
        }
 
        public void CreateEmployee(EmployeeModel employeeDto)
        {
 
            _unitOfWork.Employees.Add(employeeDto);
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
 
 
    }
}