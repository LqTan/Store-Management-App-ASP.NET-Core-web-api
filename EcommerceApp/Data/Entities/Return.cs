using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Data.Entities
{
    [Table("Returns")]
    public class Return
    {
        [Key]
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [MaxLength]
        public string? Reason { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public int? Status { get; set; }
        public Order? Order { get; set; }
    }
}
