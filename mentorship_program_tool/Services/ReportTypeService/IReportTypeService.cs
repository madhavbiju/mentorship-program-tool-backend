using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Services.ReportTypeService
{
    public interface IReportTypeService
    {
        IEnumerable<ReportTypeModel> GetReportType();
        ReportTypeModel GetReportTypeById(int id);
        void CreateReportType(ReportTypeModel ReportType);
        void UpdateReportType(int id, ReportTypeModel ReportType);
        void DeleteReport(int id);
    }

}

