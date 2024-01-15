// IEmployeeService.cs
using System.Collections.Generic;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services
{
    public interface IEmployeeService
    {
        IEnumerable<EmployeeModel> GetEmployee();
        EmployeeModel GetEmployeeById(int id);
        void CreateEmployee(EmployeeModel employee);
        void DeleteEmployee(int id);
    }
}
