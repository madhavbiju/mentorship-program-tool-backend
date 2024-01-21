using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class MentorRequestModel
    {
        [Key]
        public int programextensionid { get; set; }
        public int programid { get; set; }
        public int? modifiedby { get; set; }
        public DateTime createdtime { get; set; }
        public DateTime newenddate { get; set; }
        public string reason { get; set; }
        public int requeststatusid { get; set; }


    }
}
