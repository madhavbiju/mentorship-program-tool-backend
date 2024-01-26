namespace mentorship_program_tool.Models.ApiModel
{
    public class GetTasksByProgramIdAPIModel
    {
        public int taskid { get; set; }
        public string taskname { get; set; }
        public string menteefirstname { get; set; }
        public string menteelastname { get; set; }
        public string mentorfirstname { get; set; }
        public string mentorlastname { get; set; }

        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public int taskstatus { get; set; }
    }
}
