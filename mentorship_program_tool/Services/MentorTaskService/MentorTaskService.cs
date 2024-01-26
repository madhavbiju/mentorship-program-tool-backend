using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System.Threading.Tasks;

namespace mentorship_program_tool.Services.MentorTaskRepository
{
    public class MentorTaskService : IMentorTaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MentorTaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void CreateTask(MentorTaskAPIModel mentortaskapimodel)
        {
            var request = MapToProgramExtension(mentortaskapimodel);
            _unitOfWork.mentorTaskRepository.Add(request);
            _unitOfWork.Complete();
        }
        private TaskModel MapToProgramExtension(MentorTaskAPIModel mentortaskapimodel)
        {
            return new TaskModel
            {
                programid = mentortaskapimodel.programid,
                title = mentortaskapimodel.title,
                taskdescription = mentortaskapimodel.taskdescription,
                startdate = mentortaskapimodel.startdate,
                enddate = mentortaskapimodel.enddate,
                referencematerialfilepath = mentortaskapimodel.referencematerialfilepath,
                filepath = null,
                submissiontime = null,
                taskstatus = 4,
                createdby = mentortaskapimodel.createdby
            };
        }


        public void UpdateStatusOfTask(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel)
        {
            var existingTask = _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.taskstatus = 3;

            _unitOfWork.Complete();
        }

        public void UpdateEndDateOfTask(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel)
        {
            var existingTask = _unitOfWork.mentorTaskRepository.GetById(id);

            if (existingTask == null)
            {
                return;
            }

            // Update properties based on adminapi model
            existingTask.modifiedtime = taskenddateupdationmodel.modifiedtime;

            _unitOfWork.Complete();

        }
    }
}
