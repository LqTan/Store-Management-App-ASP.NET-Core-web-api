using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public PaymentRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(PaymentModel payment)
        {
            var newPayment = mapper.Map<Payment>(payment);
            context.Payments!.Add(newPayment);
            await context.SaveChangesAsync();
            return newPayment.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deletePayment = context.Payments!.SingleOrDefault(x => x.Id == id);
            if (deletePayment != null)
            {
                context.Payments!.Remove(deletePayment);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<PaymentModel>> GetAllAsync()
        {
            var payment = await context.Payments!.ToListAsync();
            return mapper.Map<List<PaymentModel>>(payment);
        }

        public async Task<PaymentModel> GetByIdAsync(int id)
        {
            var payment = await context.Payments!.FindAsync(id);
            return mapper.Map<PaymentModel>(payment);
        }

        public async Task UpdateAsync(int id, PaymentModel payment)
        {
            if (id == payment.Id)
            {
                var updatePayment = mapper.Map<Payment>(payment);
                context.Payments!.Update(updatePayment);
                await context.SaveChangesAsync();
            }
        }
    }
}
