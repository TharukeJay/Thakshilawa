using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.ViewModels
{
    public class cafeteriaViewModel
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }

        public DateTime Date { get; set; }

        public Decimal Income { get; set; }

        public Decimal Expenditure { get; set; }
    }
}
