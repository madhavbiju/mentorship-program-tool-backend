using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace mentorship_program_tool.Services.GetTasksbyEmployeeIdService
{
    public class GetTasksByEmployeeIdService : IGetTasksbyEmployeeIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetTasksByEmployeeIdService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<GetTasksByEmployeeIdAPIModel> GetTasksByEmployeeId(int ID, int status, int page)
        {
            int pageSize = 5;
            int offset = (page - 1) * pageSize;

            IQueryable<GetTasksByEmployeeIdAPIModel> query = from task in _context.Tasks
                                                             join program in _context.Programs on task.ProgramID equals program.ProgramID
                                                             join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                                                             join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                                                             where mentor.EmployeeID == ID
                                                             select new GetTasksByEmployeeIdAPIModel
                                                             {
                                                                 TaskID = task.TaskID,
                                                                 MentorFirstName = mentor.FirstName,
                                                                 MentorLastName = mentor.LastName,
                                                                 MenteeFirstName = mentee.FirstName,
                                                                 MenteeLastName = mentee.LastName,
                                                                 StartDate = program.StartDate,
                                                                 EndDate = program.EndDate,
                                                                 TaskName = task.Title,
                                                                 TaskStatus = task.TaskStatus
                                                             };

            if (status >= 1)
            {
                query = query.Where(task => task.TaskStatus == status);
            }

            // Apply pagination
            query = query.Skip(offset).Take(pageSize);

            return query.ToList();
        }

        
    }
}
