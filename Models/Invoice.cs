using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XYZLaundry.Models
{
    public class Invoice : ModelBase
    {
        public Invoice()
        {
            CreatedOn = DateTime.UtcNow;
            InvoiceNumber = DateTime.UtcNow.Date.Year.ToString() +
                DateTime.UtcNow.Date.Month.ToString() +
                DateTime.UtcNow.Date.Day.ToString() + Guid.NewGuid().ToString().Substring(0, 4).ToUpper() + "INV";
            DueDate = DateTime.UtcNow.Date.AddMonths(1);
            SubTotal = 0;
            TaxAmount = 0;
            Discount = 0;
            Shipping = 0;
            GrandTotal = 0;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }

        [Required]
        public string InvoiceNumber { get; set; }

        public DateTime DueDate { get; set; }

        public int OrderId { get; set; }

        public string NoteToRecipient { get; set; }

        public string TermsAndCondition { get; set; }

        [Column(TypeName = "money")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "money")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "money")]
        public decimal Discount { get; set; }

        [Column(TypeName = "money")]
        public decimal Shipping { get; set; }
        
        [Column(TypeName = "money")]
        public decimal GrandTotal { get; set; }

        public bool IsPaid { get; set; }

        public virtual Order Order { get; set; }
    }
}
