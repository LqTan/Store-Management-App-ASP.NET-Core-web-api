using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public WarehouseRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(WarehouseModel warehouse)
        {
            var newWarehouse = mapper.Map<Warehouse>(warehouse);
            context.Warehouses!.Add(newWarehouse);
            await context.SaveChangesAsync();
            return newWarehouse.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteWarehouse = context.Warehouses!.SingleOrDefault(w => w.Id == id);
            if (deleteWarehouse != null)
            {
                context.Warehouses!.Remove(deleteWarehouse);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<WarehouseModel>> GetAllAsync()
        {
            var warehouse = await context.Warehouses!.ToListAsync();
            return mapper.Map<List<WarehouseModel>>(warehouse);
        }

        public async Task<WarehouseModel> GetByIdAsync(int id)
        {
            var warehouse = await context.Warehouses!.FindAsync(id);
            return mapper.Map<WarehouseModel>(warehouse);
        }

        public async Task UpdateAsync(int id, WarehouseModel warehouse)
        {
            if (id == warehouse.Id)
            {
                var updateWarehouse = mapper.Map<Warehouse>(warehouse);
                context.Warehouses!.Update(updateWarehouse);
                await context.SaveChangesAsync();
            }
        }
    }
}
