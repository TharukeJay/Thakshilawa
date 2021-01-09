using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.ViewModels
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
    }
}
