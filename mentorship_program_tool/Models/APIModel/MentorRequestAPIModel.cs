using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorRequestAPIModel
    {
        [Key]
        public int programextensionid { get; set; }
        public int programid { get; set; }
        public DateTime newenddate { get; set; }
        public string reason { get; set; }
        public int modifiedby { get; set; }
        public int RequestStatusId { get; set; } = 2;


        //created time 
    }
}
