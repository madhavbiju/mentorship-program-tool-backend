using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.StatusService
{
    public class StatusService : IStatusService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Status>> GetStatus()
        {

            var status = await _unitOfWork.Status.GetAll();
            return status;
        }

        public async Task<Status> GetStatusById(int id)
        {
            var status = await _unitOfWork.Status.GetById(id);
            return status;
        }

        public async Task CreateStatus(Status statusDto)
        {

            await _unitOfWork.Status.Add(statusDto);
            _unitOfWork.Complete();
        }

        public async void UpdateStatus(int id, Status statusDto)
        {
            var existingStatus = await _unitOfWork.Status.GetById(id);

            if (existingStatus == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on statusDto
            existingStatus.StatusValue = statusDto.StatusValue;


            _unitOfWork.Complete();
        }

        public async void DeleteStatus(int id)
        {
            var status = await _unitOfWork.Status.GetById(id);

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
