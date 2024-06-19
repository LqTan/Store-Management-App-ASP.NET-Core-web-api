using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]       
        public int Id { get; set; }
        public string? UserId { get; set; }
        public DateOnly? OrderDate { get; set; }
        public int? OrderStatus { get; set; } = 0;
        public ApplicationUser? OrderUser { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        public ICollection<Return> Returns { get; set; } = new List<Return>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
