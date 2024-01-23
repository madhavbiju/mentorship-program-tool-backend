using mentorship_program_tool.Models.ApiModel;

namespace mentorship_program_tool.Services.GetMenteeDetailsById
{
    public interface IGetMenteeDetailsByIdService
    {
        public GetMenteeDetailsByIdAPIModel GetDetailsById(int id);

    }
}
