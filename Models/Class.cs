using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Models;

namespace Thakshilawa.Models
{
    public class Class : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        public string Duration { get; set; }

        public int LecturerID { get; set; }

        public int SessionID { get; set; }
    }
}
