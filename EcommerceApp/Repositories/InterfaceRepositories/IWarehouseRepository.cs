using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IWarehouseRepository
    {
        public Task<List<WarehouseModel>> GetAllAsync();
        public Task<WarehouseModel> GetByIdAsync(int id);
        public Task<int> AddAsync(WarehouseModel warehouse);
        public Task UpdateAsync(int id, WarehouseModel warehouse);
        public Task DeleteAsync(int id);
    }
}
