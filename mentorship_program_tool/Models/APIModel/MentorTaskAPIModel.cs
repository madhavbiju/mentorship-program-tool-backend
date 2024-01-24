using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.APIModel
{
    public class MentorTaskAPIModel
    {
        [Key]
        public int taskid { get; set; }
        public int programid { get; set; }
        public string title { get; set; }
        public string taskdescription { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string referencematerialfilepath { get; set; }
        public int taskstatus { get; set; }  //set 2-pending
        public int createdby { get; set; }
    }
}
