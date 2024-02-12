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
        private Models.EntityModel.Task MapToProgramExtension(MentorTaskAPIModel mentortaskapimodel)
        {
            return new Models.EntityModel.Task
            {
                ProgramID = mentortaskapimodel.ProgramID,
                Title = mentortaskapimodel.Title,
                TaskDescription = mentortaskapimodel.TaskDescription,
                StartDate = mentortaskapimodel.StartDate,
                EndDate = mentortaskapimodel.EndDate,
                ReferenceMaterialFilePath = mentortaskapimodel.ReferenceMaterialFilePath,
                FilePath = null,
                SubmissionTime = null,
                TaskStatus = 1,
                CreatedBy = mentortaskapimodel.CreatedBy
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
            existingTask.TaskStatus = 3;

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
            existingTask.ModifiedTime = taskenddateupdationmodel.ModifiedTime;

            _unitOfWork.Complete();

        }
    }
}
