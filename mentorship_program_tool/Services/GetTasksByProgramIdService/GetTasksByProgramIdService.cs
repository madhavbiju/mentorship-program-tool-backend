﻿using Humanizer;
using mentorship_program_tool.Data;
using mentorship_program_tool.UnitOfWork;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Collections.Generic;
using System.Linq;
using mentorship_program_tool.Models.ApiModel;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;

namespace mentorship_program_tool.Services.GetActiveTasks
{
    public class GetTasksByProgramIdService : IGetTasksByProgramIdService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetTasksByProgramIdService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IEnumerable<GetTasksByProgramIdAPIModel> GetTasksByProgramId(int ID, int status)
        {
            var tasksQuery = from task in _context.Tasks
                             join program in _context.Programs on task.ProgramID equals program.ProgramID
                             join mentor in _context.Employees on program.MentorID equals mentor.EmployeeID
                             join mentee in _context.Employees on program.MenteeID equals mentee.EmployeeID
                             where task.TaskStatus == status && task.ProgramID == ID
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

            return tasksQuery.ToList();
        }
    }
}
