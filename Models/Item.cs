using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XYZLaundry.Models
{
    public class Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal ItemPrice { get; set; }
    }
}
