using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IDiscountRepository
    {
        public Task<List<DiscountModel>> GetAllAsync();
        public Task<DiscountModel> GetByIdAsync(int id);
        public Task<int> AddAsync(DiscountModel discount);
        public Task UpdateAsync(int id, DiscountModel discount);
        public Task DeleteAsync(int id);
    }
}
