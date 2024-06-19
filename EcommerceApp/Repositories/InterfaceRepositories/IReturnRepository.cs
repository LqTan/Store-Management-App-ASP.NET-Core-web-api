using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IReturnRepository
    {
        public Task<List<ReturnModel>> GetAllAsync();
        public Task<ReturnModel> GetByIdAsync(int id);
        public Task<int> AddAsync(ReturnModel @return);
        public Task UpdateAsync(int id, ReturnModel @return);
        public Task DeleteAsync(int id);
    }
}
