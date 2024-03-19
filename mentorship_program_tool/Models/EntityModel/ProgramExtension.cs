using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class ProgramExtension
    {
        [Key]
        public int ProgramExtensionID { get; set; }

        public int ProgramID { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime NewEndDate { get; set; }

        public string Reason { get; set; }

        public int? RequestStatusID { get; set; }


    }
}
