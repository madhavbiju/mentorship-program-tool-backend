using mentorship_program_tool.Repository;

namespace mentorship_program_tool.UnitOfWork
{
    public interface IUnitOfWork
    {
        IEmployeeRepository Employees { get; }

        int Complete();
    }
}
