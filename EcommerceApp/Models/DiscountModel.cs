namespace EcommerceApp.Models
{
    public class DiscountModel
    {
        public int Id { get; set; }
        public string? DiscountCode { get; set; }
        public DateOnly? DiscountEffectiveStart { get; set; }
        public DateOnly? DiscountEffectiveEnd { get; set; }
        public int? DiscountPercent { get; set; }
    }
}
