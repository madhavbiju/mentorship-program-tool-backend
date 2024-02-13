using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using System;
using System.Linq;

namespace mentorship_program_tool.Services.GetTasksbyEmployeeIdService
{
    public class GetTasksByEmployeeIdService : IGetTasksbyEmployeeIdService
    {
        private readonly AppDbContext _context;

        public GetTasksByEmployeeIdService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetTasksByEmployeeIdResponseAPIModel GetTasksByEmployeeId(int employeeId, int status, int page, string sortBy)
        {
            int pageSize = 5;
            int offset = (page - 1) * pageSize;

            IQueryable<GetTasksByEmployeeIdAPIModel> query = from task in _context.Tasks
                                                             join program in _context.Programs on task.ProgramID equals program.ProgramID
                                                             join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                                                             join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                                                             where employeeId == 0 || mentor.EmployeeID == employeeId
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

            // Filter by task status if status is provided
            if (status >= 1)
            {
                query = query.Where(task => task.TaskStatus == status);
            }

            // Apply sorting
            switch (sortBy)
            {
                case "TaskName":
                    query = query.OrderBy(p => p.TaskName); // Ascending order by default
                    break;
                case "TaskName_desc":
                    query = query.OrderByDescending(p => p.TaskName); // Descending order for ProgramName
                    break;
                case "endDate":
                    query = query.OrderBy(p => p.EndDate); // Ascending order by default
                    break;
                case "endDate_desc":
                    query = query.OrderByDescending(p => p.EndDate); // Descending order for EndDate
                    break;
            }

            int totalCount = query.Count();

            // Apply pagination
            query = query.Skip(offset).Take(pageSize);

            return new GetTasksByEmployeeIdResponseAPIModel { Tasks = query.ToList(), TotalCount = totalCount };
        }

    }
}
