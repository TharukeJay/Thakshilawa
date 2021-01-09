using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XYZLaundry.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }

        public int Qty { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }
    }
}
