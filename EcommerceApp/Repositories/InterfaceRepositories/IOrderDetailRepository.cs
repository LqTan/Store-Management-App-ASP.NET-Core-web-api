using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IOrderDetailRepository
    {
        public Task<List<OrderDetailModel>> GetAllAsync();
        public Task<OrderDetailModel> GetByIdAsync(int id);
        public Task<int> AddAsync(OrderDetailModel orderDetail);
        public Task UpdateAsync(int id, OrderDetailModel orderDetail);
        public Task DeleteAsync(int id);
    }
}
