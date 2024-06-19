using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Discounts")]
    public class Discount
    {
        [Key]        
        public int Id { get; set; }
        public string? DiscountCode { get; set; }
        public DateOnly? DiscountEffectiveStart { get; set; }
        public DateOnly? DiscountEffectiveEnd { get; set; }
        public int? DiscountPercent { get; set; }
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
