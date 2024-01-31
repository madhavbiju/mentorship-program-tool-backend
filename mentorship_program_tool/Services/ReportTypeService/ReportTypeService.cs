using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services.ReportTypeService
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReportType> GetReportType()
        {

            var ReportType = _unitOfWork.ReportType.GetAll();
            return ReportType;
        }

        public ReportType GetReportTypeById(int id)
        {
            var ReportType = _unitOfWork.ReportType.GetById(id);
            return ReportType;
        }

        public void CreateReportType(ReportType ReportTypeDto)
        {

            _unitOfWork.ReportType.Add(ReportTypeDto);
            _unitOfWork.Complete();
        }

        public void UpdateReportType(int id, ReportType ReportTypeDto)
        {
            var existingReportType = _unitOfWork.ReportType.GetById(id);

            if (existingReportType == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on ReportTypeDto
            existingReportType.ReportName = ReportTypeDto.ReportName;


            _unitOfWork.Complete();
        }

        public void DeleteReport(int id)
        {
            var ReportType = _unitOfWork.ReportType.GetById(id);

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
