using System.Collections.Generic;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetEmployees(); // Updated method name to reflect returning multiple employees
        Task<Employee> GetEmployeeById(int id);
        // IEnumerable<GetAllMentorsAPIModel> GetAllMentors(); //Name of all mentors
        System.Threading.Tasks.Task CreateEmployee(Employee employee);
        void DeleteEmployee(int id);
    }
}
