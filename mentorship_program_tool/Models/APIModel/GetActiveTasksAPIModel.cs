namespace mentorship_program_tool.Models.ApiModel
{
    public class GetActiveTasksAPIModel
    {
        public int taskid { get; set; }
        public string taskname { get; set; }
        public string menteename { get; set; }
        public string mentorname { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public string taskstatus { get; set; }
    }
}
