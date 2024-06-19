using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IInventoryRepository
    {
        public Task<List<InventoryModel>> GetAllAsync();
        public Task<InventoryModel> GetByIdAsync(int id);
        public Task<int> AddAsync(InventoryModel inventory);
        public Task UpdateAsync(int id, InventoryModel inventory);
        public Task DeleteAsync(int id);
    }
}
