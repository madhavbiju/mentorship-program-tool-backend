using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mentorship_program_tool.Models.EntityModel
{
    public class ProgramModel
    {
        [Key]
        public int programid { get; set; }

        public int MentorId { get; set; }
        public int MenteeId { get; set; }

        public int createdby { get; set; }

        public DateTime createdtime { get; set; }

        public int? modifiedby { get; set; }

        public DateTime? modifiedtime { get; set; }

        public DateTime startdate { get; set; }

        public DateTime enddate { get; set; }

        public string programname { get; set; }
        public int programstatus { get; set; }


    }
}