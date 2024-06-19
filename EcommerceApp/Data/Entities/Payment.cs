using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Payments")]
    public class Payment
    {
        [Key]        
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string? Method { get; set; }
        public int? Amount { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public int? Status { get; set; }
        public int? DiscountId { get; set; }
        public Discount? Discount { get; set; }
        public Order? Order { get; set; }
    }
}
