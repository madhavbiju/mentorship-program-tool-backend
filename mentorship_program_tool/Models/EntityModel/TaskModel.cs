using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class TaskModel
    {
        [Key]
        public int taskid { get; set; }
        public int programid { get; set; }
        public string title { get; set; }
        public string taskdescription { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string referencematerialfilepath { get; set; }
        public string? filepath { get; set; }
        public DateTime? submissiontime { get; set; }
        public string taskstatus { get; set; }
        public int createdby { get; set; }
        public DateTime? modifiedtime { get; set; }
    }
}
