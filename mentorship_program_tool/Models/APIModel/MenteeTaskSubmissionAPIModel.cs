using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MenteeTaskSubmissionAPIModel
    {
        [Key]
        public int taskid { get; set; }
        public string filepath { get; set; }
        public DateTime submissiontime { get; set; }
    }
}
