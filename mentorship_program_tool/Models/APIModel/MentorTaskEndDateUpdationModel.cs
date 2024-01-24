using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorTaskEndDateUpdationModel
    {
        [Key]
        public int taskid { get; set; }
        public DateTime modifiedtime { get; set; }
    }
}
