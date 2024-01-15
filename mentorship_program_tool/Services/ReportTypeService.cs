using mentorship_program_tool.Models.EntityModel;
using mentorship_program_tool.UnitOfWork;

namespace mentorship_program_tool.Services
{
    public class ReportTypeService : IReportTypeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportTypeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReportTypeModel> GetReportType()
        {

            var ReportType = _unitOfWork.ReportType.GetAll();
            return (ReportType);
        }

        public ReportTypeModel GetReportTypeById(int id)
        {
            var ReportType = _unitOfWork.ReportType.GetById(id);
            return (ReportType);
        }

        public void CreateReportType(ReportTypeModel ReportTypeDto)
        {

            _unitOfWork.ReportType.Add(ReportTypeDto);
            _unitOfWork.Complete();
        }

        public void UpdateReportType(int id, ReportTypeModel ReportTypeDto)
        {
            var existingReportType = _unitOfWork.ReportType.GetById(id);

            if (existingReportType == null)
            {
                // Handle not found scenario
                return;
            }

            // Update properties based on ReportTypeDto
            existingReportType.reporttype = ReportTypeDto.reporttype;


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
