using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class AdminApprovalAPIModel
    {
        [Key]
        public int ProgramExtensionID { get; set; }

        public int? ModifiedBy { get; set; }

        public int RequestStatusID { get; set; }
    }
}
