using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XYZLaundry.ViewModels
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }

        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }

        [Display(Name = "Invoice Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime DueDate { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Order Number")]
        public string OrderNumber { get; set; }

        [Display(Name = "Note To Recipient")]
        public string NoteToRecipient { get; set; }

        [Display(Name = "Terms And Condition")]
        public string TermsAndCondition { get; set; }

        [Display(Name = "Sub Total")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Shipping")]
        public decimal Shipping { get; set; }

        [Display(Name = "Grand Total")]
        public decimal GrandTotal { get; set; }

        [Display(Name = "Already Paid")]
        public bool IsPaid { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        public CustomerViewModel Customer { get; set; }

        public OrdersViewModel Order { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }


    }
}
