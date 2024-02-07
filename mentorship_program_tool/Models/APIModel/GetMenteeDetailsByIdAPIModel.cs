using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.ApiModel
{
    public class GetMenteeDetailsByIdAPIModel
    {
        [Key]
        public int ProgramID { get; set; }

        public string MentorFirstName { get; set; }

        public string MenteeFirstName { get; set; }

        public string MenteeLastName { get; set; }

        public string ProgramName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
