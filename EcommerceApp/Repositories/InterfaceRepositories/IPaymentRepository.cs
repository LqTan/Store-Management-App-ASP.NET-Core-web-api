using EcommerceApp.Models;

namespace EcommerceApp.Repositories.InterfaceRepositories
{
    public interface IPaymentRepository
    {
        public Task<List<PaymentModel>> GetAllAsync();
        public Task<PaymentModel> GetByIdAsync(int id);
        public Task<int> AddAsync(PaymentModel payment);
        public Task UpdateAsync(int id, PaymentModel payment);
        public Task DeleteAsync(int id);
    }
}
