using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MenteeTaskSubmissionAPIModel
    {
        [Key]
        public int TaskID { get; set; }
        public string FilePath { get; set; }
        public DateTime SubmissionTime { get; set; }
    }
}
