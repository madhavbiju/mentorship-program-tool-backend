using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.ApiModel
{
    public class GetTasksByEmployeeIdAPIModel
    {
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public string MenteeFirstName { get; set; }
        public string MenteeLastName { get; set; }
        public string MentorFirstName { get; set; }
        public string MentorLastName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TaskStatus { get; set; }
    }
}
