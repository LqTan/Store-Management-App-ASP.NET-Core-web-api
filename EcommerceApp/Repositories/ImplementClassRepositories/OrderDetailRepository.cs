using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public OrderDetailRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(OrderDetailModel orderDetail)
        {
            var newOrderDetail = mapper.Map<OrderDetail>(orderDetail);
            context.OrderDetails!.Add(newOrderDetail);
            await context.SaveChangesAsync();
            return newOrderDetail.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteOrderDetail = context.OrderDetails!.SingleOrDefault(o => o.Id == id);
            if (deleteOrderDetail != null)
            {
                context.OrderDetails!.Remove(deleteOrderDetail);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<OrderDetailModel>> GetAllAsync()
        {
            var orderDetail = await context.OrderDetails!.ToListAsync();
            return mapper.Map<List<OrderDetailModel>>(orderDetail);
        }

        public async Task<OrderDetailModel> GetByIdAsync(int id)
        {
            var orderDetail = await context.OrderDetails!.FindAsync(id);
            return mapper.Map<OrderDetailModel>(orderDetail);
        }

        public async Task UpdateAsync(int id, OrderDetailModel orderDetail)
        {
            if (id == orderDetail.Id)
            {
                var updateOrderDetail = mapper.Map<OrderDetail>(orderDetail);
                context.OrderDetails!.Update(updateOrderDetail);
                await context.SaveChangesAsync();
            }
        }
    }
}
