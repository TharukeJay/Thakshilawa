using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.ViewModels
{
    public class staffViewModel
    {
        public int StaffId { get; set; }
        public string Name { get; set; }

        public string NICNo { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Designation { get; set; }

        public string ReportingPoint { get; set; }
        public string Department { get; set; }

        public int ContactNumber { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }
        public string Branch { get; set; }
    }
}
