using System;
using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class Task
    {
        [Key]
        public int TaskID { get; set; }

        public int ProgramID { get; set; }

        public string Title { get; set; }

        public string TaskDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string ReferenceMaterialFilePath { get; set; }

        public string? FilePath { get; set; }

        public DateTime? SubmissionTime { get; set; }

        public int TaskStatus { get; set; }

        public int CreatedBy { get; set; }

        public DateTime? ModifiedTime { get; set; }
    }
}
