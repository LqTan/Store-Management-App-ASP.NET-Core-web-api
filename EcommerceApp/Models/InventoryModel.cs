namespace EcommerceApp.Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? WarehouseId { get; set; }
        public int? Quantity { get; set; } = 0;
    }
}
