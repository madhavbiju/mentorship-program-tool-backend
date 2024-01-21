using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class AdminApprovalAPIModel
    {
        [Key]
        public int programextensionid { get; set; }
        public int? modifiedby { get; set; }
        public int requeststatusid { get; set; }
    }
}
