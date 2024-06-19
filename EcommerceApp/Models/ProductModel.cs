using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }
        [MaxLength]
        public string? Description { get; set; }
    }
}
