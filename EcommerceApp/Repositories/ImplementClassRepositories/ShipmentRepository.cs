using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public ShipmentRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ShipmentModel shipment)
        {
            var newShipment = mapper.Map<Shipment>(shipment);
            context.Shipments!.Add(newShipment);
            await context.SaveChangesAsync();
            return newShipment.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteShipment = context.Shipments!.SingleOrDefault(s => s.Id == id);
            if (deleteShipment != null)
            {
                context.Shipments!.Remove(deleteShipment);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ShipmentModel>> GetAllAsync()
        {
            var shipment = await context.Shipments!.ToListAsync();
            return mapper.Map<List<ShipmentModel>>(shipment);
        }

        public async Task<ShipmentModel> GetByIdAsync(int id)
        {
            var shipment = await context.Shipments!.FindAsync(id);
            return mapper.Map<ShipmentModel>(shipment);
        }

        public async Task UpdateAsync(int id, ShipmentModel shipment)
        {
            if (id == shipment.Id)
            {
                var updateShipment = mapper.Map<Shipment>(shipment);
                context.Shipments!.Update(updateShipment);
                await context.SaveChangesAsync();
            }
        }
    }
}
