using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IBrandRepository
    {
        public Task<List<BrandModel>> GetAllAsync();
        public Task<BrandModel> GetByIdAsync(int id);
        public Task<int> AddAsync(BrandModel brand);
        public Task UpdateAsync(int id, BrandModel brand);
        public Task DeleteAsync(int id);
    }
}
