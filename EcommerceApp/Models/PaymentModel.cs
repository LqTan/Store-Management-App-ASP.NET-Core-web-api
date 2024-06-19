namespace EcommerceApp.Models
{
    public class PaymentModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public string? Method { get; set; }
        public int? Amount { get; set; }
        public DateOnly? PaymentDate { get; set; }
        public int? Status { get; set; }
        public int? DiscountId { get; set; }
    }
}
