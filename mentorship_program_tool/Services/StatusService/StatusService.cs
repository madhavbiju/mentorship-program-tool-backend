using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.StatusService
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Status> GetStatus()
        {

            var status = _unitOfWork.Status.GetAll();
            return status;
        }

        public Status GetStatusById(int id)
        {
            var status = _unitOfWork.Status.GetById(id);
            return status;
        }

        public void CreateStatus(Status statusDto)
        {

            _unitOfWork.Status.Add(statusDto);
            _unitOfWork.Complete();
        }

        public void UpdateStatus(int id, Status statusDto)
        {
            var existingStatus = _unitOfWork.Status.GetById(id);

            if (existingStatus == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on statusDto
            existingStatus.StatusValue = statusDto.StatusValue;


            _unitOfWork.Complete();
        }

        public void DeleteStatus(int id)
        {
            var status = _unitOfWork.Status.GetById(id);

            if (status == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.Status.Delete(status);
            _unitOfWork.Complete();
        }


    }
}
