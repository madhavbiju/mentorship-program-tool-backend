using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.ReportTypeService
{
    public interface IReportTypeService
    {
        IEnumerable<ReportType> GetReportType();
        ReportType GetReportTypeById(int id);
        void CreateReportType(ReportType ReportType);
        void UpdateReportType(int id, ReportType ReportType);
        void DeleteReport(int id);
    }

}

