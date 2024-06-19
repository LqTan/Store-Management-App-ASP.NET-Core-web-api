using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class ShipmentModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [MaxLength]
        public string? Address { get; set; }
        public DateOnly? DeliveryDate { get; set; }
        public int? CarrierId { get; set; }
    }
}
