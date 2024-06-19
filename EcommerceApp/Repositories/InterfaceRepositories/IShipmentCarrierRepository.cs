using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IShipmentCarrierRepository
    {
        public Task<List<ShipmentCarrierModel>> GetAllAsync();
        public Task<ShipmentCarrierModel> GetByIdAsync(int id);
        public Task<int> AddAsync(ShipmentCarrierModel shipmentCarrier);
        public Task UpdateAsync(int id, ShipmentCarrierModel shipmentCarrier);
        public Task DeleteAsync(int id);
    }
}
