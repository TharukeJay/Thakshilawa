using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XYZLaundry.Models;

namespace Thakshilawa.Models
{
    public class Cafeteria : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int EmployeeID { get; set; }

        public DateTime Date { get; set; }

        public Decimal Income { get; set; }

        public Decimal Expenditure { get; set; }


    }
}
