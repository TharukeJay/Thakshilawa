using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XYZLaundry.Models.Enum;

namespace XYZLaundry.Models
{
    public class Order : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        public string OrderNumber { get; set; }

        public string Items { get; set; }

        public string Notes { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int CustomerId { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public ServiceType ServiceType { get; set; }

        public PaymentType PaymentType { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public CollectionType CollectionType { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
