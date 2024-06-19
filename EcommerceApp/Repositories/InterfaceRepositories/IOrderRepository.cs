using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IOrderRepository
    {
        public Task<List<OrderModel>> GetAllAsync();
        public Task<OrderModel> GetByIdAsync(int id);
        public Task<int> AddAsync(OrderModel order);
        public Task UpdateAsync(int id, OrderModel order);
        public Task DeleteAsync(int id);
    }
}
