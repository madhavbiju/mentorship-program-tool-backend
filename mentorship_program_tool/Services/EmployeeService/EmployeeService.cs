using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;

namespace mentorship_program_tool.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _unitOfWork.Employee.GetAll();
        }

        public Employee GetEmployeeById(int id)
        {
            return _unitOfWork.Employee.GetById(id);
        }

        public void CreateEmployee(Employee employee)
        {
            _unitOfWork.Employee.Add(employee);
            _unitOfWork.Complete();
        }

        public void DeleteEmployee(int id)
        {
            var employee = _unitOfWork.Employee.GetById(id);

            if (employee == null)
            {
                return;
            }

            _unitOfWork.Employee.Delete(employee);
            _unitOfWork.Complete();
        }
    }
}
