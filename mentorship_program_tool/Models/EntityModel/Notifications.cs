using System.ComponentModel.DataAnnotations;

namespace mentorship_program_tool.Models.EntityModel
{
    public class Notifications
    {
        [Key]
        public int NotificationID { get; set; }

        public int NotifiedEmployeeID { get; set; }
        public string Notification { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
