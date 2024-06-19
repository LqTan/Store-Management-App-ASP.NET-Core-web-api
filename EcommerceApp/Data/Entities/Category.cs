using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }        
        public string? Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
