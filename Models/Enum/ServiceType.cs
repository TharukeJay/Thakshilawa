using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.Models.Enum
{
    public enum ServiceType
    {
        [Display(Name = "Ironing")]
        Iron = 1,

        [Display(Name = "Washing")]        
        Wash = 2,
        
        [Display(Name = "Dry Cleaning")]
        DryClean = 3,

        [Display(Name = "Pressing")]        
        Pressing = 4
    }
}
