using mentorship_program_tool.Data;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace mentorship_program_tool.Services.GetActiveTasks
{
    public class GetTasksByProgramIdService : IGetTasksByProgramIdService
    {
        private readonly AppDbContext _context;

        public GetTasksByProgramIdService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public GetTasksByProgramIdResponseAPIModel GetTasksByProgramId(int ID, int status, int page, string sortBy)
        {
            int pageSize = 5;
            int offset = (page - 1) * pageSize;

            IQueryable<GetTasksByProgramIdAPIModel> tasksQuery = from task in _context.Tasks
                                                                 join program in _context.Programs on task.ProgramID equals program.ProgramID
                                                                 join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                                                                 join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                                                                 where task.ProgramID == ID && (status == 0 || task.TaskStatus == status)
                                                                 select new GetTasksByProgramIdAPIModel
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

            // Apply sorting
            switch (sortBy)
            {
                case "TaskName":
                    tasksQuery = tasksQuery.OrderBy(task => task.TaskName); // Ascending order by default
                    break;
                case "TaskName_desc":
                    tasksQuery = tasksQuery.OrderByDescending(task => task.TaskName); // Descending order for TaskName
                    break;
                case "endDate":
                    tasksQuery = tasksQuery.OrderBy(task => task.EndDate); // Ascending order by default
                    break;
                case "endDate_desc":
                    tasksQuery = tasksQuery.OrderByDescending(task => task.EndDate); // Descending order for EndDate
                    break;
                // Add more cases for other sorting criteria if needed
                default:
                    tasksQuery = tasksQuery.OrderBy(task => task.TaskName); // Default sorting by TaskName
                    break;
            }
            int totalCount = tasksQuery.Count();

            // Apply pagination
            tasksQuery = tasksQuery.Skip(offset).Take(pageSize);

            return new GetTasksByProgramIdResponseAPIModel { Tasks = tasksQuery.ToList(), TotalCount = totalCount };
        }
    }
}
