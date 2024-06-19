namespace EcommerceApp.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public int? OrderStatus { get; set; } = 0;
    }
}
