using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorTaskStatusUpdationAPIModel
    {
        [Key]
        public int taskid { get; set; }
        public int taskstatus { get; set; }
    }
}
