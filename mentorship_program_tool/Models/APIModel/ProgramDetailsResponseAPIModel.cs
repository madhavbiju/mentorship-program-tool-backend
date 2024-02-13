namespace mentorship_program_tool.Models.APIModel
{
    public class ProgramDetailsResponseAPIModel
    {
        public IEnumerable<Models.EntityModel.Program> Programs { get; set; }
        public int TotalCount { get; set; }
    }
}
