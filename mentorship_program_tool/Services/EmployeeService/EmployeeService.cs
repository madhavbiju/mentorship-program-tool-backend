using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;

namespace mentorship_program_tool.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public EmployeeService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
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
