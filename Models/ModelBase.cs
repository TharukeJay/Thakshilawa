using System;
using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.Models
{
    public class ModelBase
    {
        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}
