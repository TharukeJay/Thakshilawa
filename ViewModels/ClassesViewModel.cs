using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Thakshilawa.ViewModels
{
    public class ClassesViewModel
    {

        public int ClassId { get; set; }

        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Display(Name = "Duration")]
        public string Duration { get; set; }

        [Display(Name = "Lecturer ID")]
        public int LecturerID { get; set; }

        [Display(Name = "Session ID")]
        public int SessionID { get; set; }

        [Display(Name = "Student Capacity")]
        public int StudentCapacity { get; set; }

        [Display(Name = "Monthly Fee (LKR)")]
        public decimal MonthlyFee { get; set; }

        [Display(Name = "Course Duration")]
        public int CourseDuration { get; set; }
    }
}
