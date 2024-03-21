using mentorship_program_tool.Models.APIModel;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.MentorTaskRepository
{
    public interface IMentorTaskService
    {
        void CreateTask(MentorTaskAPIModel mentortaskapimodel);
        void UpdateStatusOfTask(int id, MentorTaskStatusUpdationAPIModel taskstatusupdationmodel);

        void UpdateEndDateOfTask(int id, MentorTaskEndDateUpdationModel taskenddateupdationmodel);

    }
}
