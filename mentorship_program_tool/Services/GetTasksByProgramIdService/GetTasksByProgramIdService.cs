using Humanizer;
using mentorship_program_tool.Data;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.UnitOfWork;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.ApiModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Build.Framework;

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


        public IEnumerable<GetTasksByProgramIdAPIModel> GetTasksByProgramId(int id, int status)
        {
            if (status >= 1)
            {

                var tasks = from task in _context.task
                            join program in _context.Program on task.programid equals program.programid
                            join mentor in _context.Employee on program.MentorId equals mentor.employeeid
                            join mentee in _context.Employee on program.MenteeId equals mentee.employeeid
                            where task.taskstatus == status && task.programid == id
                            select new GetTasksByProgramIdAPIModel
                            {
                                taskid = task.taskid,
                                mentorfirstname = mentor.firstname,
                                mentorlastname = mentor.lastname,
                                menteefirstname = mentee.firstname,
                                menteelastname = mentee.lastname,
                                startdate = program.startdate,
                                enddate = program.enddate,
                                taskname = task.title,
                                taskstatus = task.taskstatus
                            };

                return tasks;
            }
            else
            {
                var tasks = from task in _context.task
                            join program in _context.Program on task.programid equals program.programid
                            join mentor in _context.Employee on program.MentorId equals mentor.employeeid
                            join mentee in _context.Employee on program.MenteeId equals mentee.employeeid
                            where task.taskstatus == status && task.programid == id
                            select new GetTasksByProgramIdAPIModel
                            {
                                taskid = task.taskid,
                                mentorfirstname = mentor.firstname,
                                mentorlastname = mentor.lastname,
                                menteefirstname = mentee.firstname,
                                menteelastname = mentee.lastname,
                                startdate = program.startdate,
                                enddate = program.enddate,
                                taskname = task.title,
                                taskstatus = task.taskstatus
                            };

                return tasks;
            }
                

        }


    }
}
