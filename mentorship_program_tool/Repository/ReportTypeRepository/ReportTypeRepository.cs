using mentorship_program_tool.Data;
using mentorship_program_tool.Models.EntityModel;

namespace mentorship_program_tool.Repository.ReportTypeRepository
{
    public class ReportTypeRepository : Repository<ReportTypeModel>, IReportTypeRepository
    {
        public ReportTypeRepository(AppDbContext context) : base(context) { }
    }
}
