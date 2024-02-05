using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mentorship_program_tool.Models.EntityModel
{
    public class Program
    {
        [Key]
        public int ProgramID { get; set; }

        public int MentorID { get; set; }

        public int MenteeID { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedTime { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ProgramName { get; set; }

        public int ProgramStatus { get; set; }
    }
}
