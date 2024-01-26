namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllMenteesOfMentorAPIModel
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string ProgramName { get; set; }
        public DateTime Startdate { get; set; }

        public DateTime Enddate { get; set; }
    }
}
