using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IShipmentRepository
    {
        public Task<List<ShipmentModel>> GetAllAsync();
        public Task<ShipmentModel> GetByIdAsync(int id);
        public Task<int> AddAsync(ShipmentModel shipment);
        public Task UpdateAsync(int id, ShipmentModel shipment);
        public Task DeleteAsync(int id);
    }
}
