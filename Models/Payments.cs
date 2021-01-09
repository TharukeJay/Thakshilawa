using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Models;

namespace Thakshilawa.Models
{
    public class Payments : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public float PaymentsId { get; set; }
 
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        public string PaymentType { get; set; }

        public string  Month { get; set; }
        public DateTime PaidOn { get; set; }
    }
}