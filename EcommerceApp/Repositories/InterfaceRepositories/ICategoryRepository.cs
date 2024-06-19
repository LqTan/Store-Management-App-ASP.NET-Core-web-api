using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface ICategoryRepository
    {
        public Task<List<CategoryModel>> GetAllAsync();
        public Task<CategoryModel> GetByIdAsync(int id);
        public Task<int> AddAsync(CategoryModel category);
        public Task UpdateAsync(int id, CategoryModel category);
        public Task DeleteAsync(int id);
    }
}
