using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.ViewModels
{
    public class studentViewModel
    {
        public int StudentId { get; set; }

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

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "Guardian Name")]
        public string GuardianName { get; set; }

        [Display(Name = "Guardian's Contact Number")]
        public string GuardianContactNumber { get; set; }
        [Display(Name = "Guardian's Address")]
        public string GuardianAddress { get; set; }
    }
}
