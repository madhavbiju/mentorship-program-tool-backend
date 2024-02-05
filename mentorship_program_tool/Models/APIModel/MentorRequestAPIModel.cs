using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorRequestAPIModel
    {
        [Key]
        public int ProgramExtensionID { get; set; }
        public int ProgramID { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Reason { get; set; }
        public int? ModifiedBy { get; set; }
        public int RequestStatusID { get; set; } = 2;
    }
}
