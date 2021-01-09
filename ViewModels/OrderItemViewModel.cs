using System.ComponentModel.DataAnnotations;
using XYZLaundry.Models;

namespace XYZLaundry.ViewModels
{
    public class OrderItemViewModel
    {
        [Display(Name = "Item")]
        [Required]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Display(Name = "Quantity")]
        [Required]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public decimal Price { get; set; }
    }
}
