using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Warehouses")]
    public class Warehouse
    {
        [Key]        
        public int Id { get; set; }        
        public string? Name { get; set; }
        [MaxLength]
        public string? Address { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
    }
}
