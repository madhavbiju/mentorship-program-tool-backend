using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorTaskEndDateUpdationModel
    {
        [Key]
        public int TaskID { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
