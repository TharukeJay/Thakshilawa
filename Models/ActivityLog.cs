using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XYZLaundry.Models
{
    public class ActivityLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityLogId { get; set; }

        [Required]
        public string Action { get; set; }

        [Required]
        public string ActionedBy { get; set; }

        [Required]
        public DateTime ActionedOn { get; set; }
    }
}
