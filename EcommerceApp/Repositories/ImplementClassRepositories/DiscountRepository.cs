using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public DiscountRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(DiscountModel discount)
        {
            var newDiscount = mapper.Map<Discount>(discount);
            context.Discounts!.Add(newDiscount);
            await context.SaveChangesAsync();
            return newDiscount.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteDiscount = context.Discounts!.SingleOrDefault(discount => discount.Id == id);
            if (deleteDiscount != null)
            {
                context.Discounts!.Remove(deleteDiscount);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<DiscountModel>> GetAllAsync()
        {
            var discount = await context.Discounts!.ToListAsync();
            return mapper.Map<List<DiscountModel>>(discount);
        }

        public async Task<DiscountModel> GetByIdAsync(int id)
        {
            var discount = await context.Discounts!.FindAsync(id);
            return mapper.Map<DiscountModel>(discount);
        }

        public async Task UpdateAsync(int id, DiscountModel discount)
        {
            if (id == discount.Id)
            {
                var updateDiscount = mapper.Map<Discount>(discount);
                context.Discounts!.Update(updateDiscount);
                await context.SaveChangesAsync();
            }
        }
    }
}
