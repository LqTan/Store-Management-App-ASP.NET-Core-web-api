using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IProductRepository
    {
        public Task<List<ProductModel>> GetAllAsync();
        public Task<ProductModel> GetByIdAsync(int id);
        public Task<int> AddAsync(ProductModel product);
        public Task UpdateAsync(int id, ProductModel product);
        public Task DeleteAsync(int id);
    }
}
