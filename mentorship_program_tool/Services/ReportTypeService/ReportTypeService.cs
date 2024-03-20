using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.ReportTypeService
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReportType>> GetReportType()
        {

            var ReportType = await _unitOfWork.ReportType.GetAll();
            return ReportType;
        }

        public async Task<ReportType> GetReportTypeById(int id)
        {
            var ReportType = await _unitOfWork.ReportType.GetById(id);
            return ReportType;
        }

        public async Task CreateReportType(ReportType ReportTypeDto)
        {

            await _unitOfWork.ReportType.Add(ReportTypeDto);
            _unitOfWork.Complete();
        }

        public async void UpdateReportType(int id, ReportType ReportTypeDto)
        {
            var existingReportType = await _unitOfWork.ReportType.GetById(id);

            if (existingReportType == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on ReportTypeDto
            existingReportType.ReportName = ReportTypeDto.ReportName;


            _unitOfWork.Complete();
        }

        public async void DeleteReport(int id)
        {
            var ReportType = await _unitOfWork.ReportType.GetById(id);

            if (ReportType == null)
            {
                // Handle not found scenario
                return;
            }

            _unitOfWork.ReportType.Delete(ReportType);
            _unitOfWork.Complete();
        }


    }
}
