using mentorship_program_tool.Models.EntityModel;
using Task = System.Threading.Tasks.Task;

namespace mentorship_program_tool.Services.ReportTypeService
{
    public interface IReportTypeService
    {
        Task<IEnumerable<ReportType>> GetReportType();
        Task<ReportType> GetReportTypeById(int id);
        Task CreateReportType(ReportType ReportType);
        void UpdateReportType(int id, ReportType ReportType);
        void DeleteReport(int id);
    }

}

