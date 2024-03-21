using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;
using Task = System.Threading.Tasks.Task;

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

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _unitOfWork.Employee.GetAll();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _unitOfWork.Employee.GetById(id);
        }

        public async Task CreateEmployee(Employee employee)
        {
            await _unitOfWork.Employee.Add(employee);
            _unitOfWork.Complete();
        }

        public async void DeleteEmployee(int id)
        {
            var employee = await _unitOfWork.Employee.GetById(id);

            if (employee == null)
            {
                return;
            }

            _unitOfWork.Employee.Delete(employee);
            _unitOfWork.Complete();
        }



    }
}
