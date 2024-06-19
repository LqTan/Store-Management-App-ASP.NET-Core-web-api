using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Models
{
    public class ReturnModel
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        [MaxLength]
        public string? Reason { get; set; }
        public DateOnly? ReturnDate { get; set; }
        public int? Status { get; set; }
    }
}
