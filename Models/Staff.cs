using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using XYZLaundry.Models;

namespace Thakshilawa.Models
{
    public class Staff
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffId { get; set; }
        public string Name { get; set; }

        public string NICNo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Designation { get; set; }

        public string ReportingPoint { get; set; }
        public string Department { get; set; }

        public int  ContactNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }
        public string Branch { get; set; }
    }
}
