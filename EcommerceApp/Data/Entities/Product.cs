using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Products")]
    public class Product
    {
        [Key]        
        public int Id { get; set; }        
        public string? Name { get; set; }        
        public double? Price { get; set; }
        public string? Image {  get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        [MaxLength]
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
