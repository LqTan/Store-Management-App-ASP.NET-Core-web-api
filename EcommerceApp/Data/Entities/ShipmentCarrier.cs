using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("ShipmentCarriers")]
    public class ShipmentCarrier
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
