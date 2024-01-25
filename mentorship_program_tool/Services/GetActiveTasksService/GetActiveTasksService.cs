using Humanizer;
using mentorship_program_tool.Data;
using mentorship_program_tool.Services.GetActiveTasksService.mentorship_program_tool.Services;
using mentorship_program_tool.UnitOfWork;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Services.GetActiveTasks
{
    public class GetActiveTasksService : IGetActiveTasksService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public GetActiveTasksService(IUnitOfWork unitOfWork, AppDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

      
            public IEnumerable<GetActiveTasksAPIModel> GetAllActiveTasks()
            {
                var tasks = from task in _context.Task
                            join program in _context.Program on task.ProgramId equals program.programid
                            join mentee in _context.Employee on program.MenteeId equals mentee.employeeid
                            join mentor in _context.Employee on program.MentorId equals mentor.employeeid
                            join status in _context.status on task.TaskStatus equals status.statusid
                            select new GetActiveTasksAPIModel
                            {
                                taskname = task.Title,
                                startdate = task.StartDate,
                                enddate = task.EndDate,
                                menteename = mentee.FirstName,
                                mentorname = mentor.FirstName,
                                taskstatus = status.StatusValue
                            };

                return tasks;
            }

        }
    }
