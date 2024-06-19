using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public OrderRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(OrderModel order)
        {
            var newOrder = mapper.Map<Order>(order);
            context.Orders!.Add(newOrder);
            await context.SaveChangesAsync();
            return newOrder.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteOrder = context.Orders!.SingleOrDefault(o => o.Id == id);
            if (deleteOrder != null)
            {
                context.Orders!.Remove(deleteOrder);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderModel>> GetAllAsync()
        {
            var order = await context.Orders!.ToListAsync();
            return mapper.Map<List<OrderModel>>(order);
        }

        public async Task<OrderModel> GetByIdAsync(int id)
        {
            var order = await context.Orders!.FindAsync(id);
            return mapper.Map<OrderModel>(order);
        }

        public async Task UpdateAsync(int id, OrderModel order)
        {
            if (id == order.Id)
            {
                var updateOrder = mapper.Map<Order>(order);
                context.Orders!.Update(updateOrder);
                await context.SaveChangesAsync();
            }
        }
    }
}
