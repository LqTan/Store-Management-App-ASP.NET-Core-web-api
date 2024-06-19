using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public InventoryRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(InventoryModel inventory)
        {
            var newInventory = mapper.Map<Inventory>(inventory);
            context.Inventories!.Add(newInventory);
            await context.SaveChangesAsync();
            return newInventory.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteInventory = context.Inventories!.SingleOrDefault(x => x.Id == id);
            if (deleteInventory != null)
            {
                context.Inventories!.Remove(deleteInventory);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<InventoryModel>> GetAllAsync()
        {
            var inventory = await context.Inventories!.ToListAsync();
            return mapper.Map<List<InventoryModel>>(inventory);
        }

        public async Task<InventoryModel> GetByIdAsync(int id)
        {
            var inventory = await context.Inventories!.FindAsync(id);
            return mapper.Map<InventoryModel>(inventory);
        }

        public async Task UpdateAsync(int id, InventoryModel inventory)
        {
            if (id == inventory.Id)
            {
                var updateInventory = mapper.Map<Inventory>(inventory);
                context.Inventories!.Update(updateInventory);
                await context.SaveChangesAsync();
            }
        }
    }
}
