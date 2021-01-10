using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Models;

namespace Thakshilawa.Models
{
    public class Classes 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClassId { get; set; }

        public string SubjectName { get; set; }

        public string Duration { get; set; }

        public int LecturerID { get; set; }

        public int SessionID { get; set; }

        public int StudentCapacity { get; set; }

        [Column(TypeName = "money")]
        public decimal MonthlyFee { get; set; }

        public int CourseDuration { get; set; }


    }
}
