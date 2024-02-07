namespace mentorship_program_tool.Models.APIModel
{
    public class GetAllMenteesOfMentorResponseAPIModel
    {
        public IEnumerable<GetAllMenteesOfMentorAPIModel> Mentees { get; set; }
        public int TotalCount { get; set; }
    }
}
