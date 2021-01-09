using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.Models.Enum
{
    public enum OrderStatus
    {
        [Display(Name = "Pending")]
        Pending = 1,

        [Display(Name = "In Progress")]
        InProgress = 2,

        [Display(Name = "Delivery")]
        Delivery = 3,

        [Display(Name = "Complete")]
        Complete = 4
    }
}
