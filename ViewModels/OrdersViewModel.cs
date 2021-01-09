using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using XYZLaundry.Models.Enum;

namespace XYZLaundry.ViewModels
{
    public class OrdersViewModel
    {
        public int OrderId { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Items")]
        public string Items { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }

        [Display(Name = "Service Type")]
        [Required]
        public ServiceType? ServiceType { get; set; }

        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }

        [Display(Name = "Collection Type")]
        [Required]
        public CollectionType? CollectionType { get; set; }

        [Display(Name = "Order Date")]        
        public DateTime OrderDate { get; set; }

        public SelectList Customers { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public OrdersViewModel()
        {
            OrderItems = new List<OrderItemViewModel>();
        }
    }
}
