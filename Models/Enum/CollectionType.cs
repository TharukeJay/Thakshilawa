using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.Models.Enum
{
    public enum CollectionType
    {
        [Display(Name = "Deliver")]
        Deliver = 1,

        [Display(Name = "Pickup")]
        Pickup = 2
    }
}
