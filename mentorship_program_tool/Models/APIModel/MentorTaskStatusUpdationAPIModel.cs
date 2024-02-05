using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorTaskStatusUpdationAPIModel
    {
        [Key]
        public int TaskID { get; set; }
        public int TaskStatus { get; set; }
    }
}
