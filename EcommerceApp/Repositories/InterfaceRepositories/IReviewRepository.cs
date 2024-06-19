using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IReviewRepository
    {
        public Task<List<ReviewModel>> GetAllAsync();
        public Task<ReviewModel> GetByIdAsync(int id);
        public Task<int> AddAsync(ReviewModel review);
        public Task UpdateAsync(int id, ReviewModel review);
        public Task DeleteAsync(int id);
    }
}
