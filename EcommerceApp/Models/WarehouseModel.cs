using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class WarehouseModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        [MaxLength]
        public string? Address { get; set; }
    }
}
