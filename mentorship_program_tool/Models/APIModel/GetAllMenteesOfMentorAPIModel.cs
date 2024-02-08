namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllMenteesOfMentorAPIModel
    {
        public int EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string ProgramName { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
