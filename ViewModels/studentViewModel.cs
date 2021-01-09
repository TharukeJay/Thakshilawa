using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.ViewModels
{
    public class studentViewModel
    {
        [Display(Name = "Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "NIC No")]
        public string NICNo { get; set; }

        [Display(Name = "Date of Birth")]
        public DateTime DateofBirth { get; set; }


        [Display(Name = "School")]
        public string School { get; set; }


        [Display(Name = "Grade")]
        public string Grade { get; set; }

        [Display(Name = "Contact Number")]
        public int ContactNumber { get; set; }
    }
}
