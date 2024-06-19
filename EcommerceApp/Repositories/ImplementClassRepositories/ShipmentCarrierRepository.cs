using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class ShipmentCarrierRepository : IShipmentCarrierRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public ShipmentCarrierRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ShipmentCarrierModel shipmentCarrier)
        {
            var newShipmentCarrier = mapper.Map<ShipmentCarrier>(shipmentCarrier);
            context.ShipmentCarriers!.Add(newShipmentCarrier);
            await context.SaveChangesAsync();
            return newShipmentCarrier.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteShipmentCarrier = context.ShipmentCarriers!.SingleOrDefault(x => x.Id == id);
            if (deleteShipmentCarrier != null)
            {
                context.ShipmentCarriers!.Remove(deleteShipmentCarrier);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ShipmentCarrierModel>> GetAllAsync()
        {
            var shipmentCarrier = await context.ShipmentCarriers!.ToListAsync();
            return mapper.Map<List<ShipmentCarrierModel>>(shipmentCarrier);
        }

        public async Task<ShipmentCarrierModel> GetByIdAsync(int id)
        {
            var shipmentCarrier = await context.ShipmentCarriers!.FindAsync(id);
            return mapper.Map<ShipmentCarrierModel>(shipmentCarrier);
        }

        public async Task UpdateAsync(int id, ShipmentCarrierModel shipmentCarrier)
        {
            if (id == shipmentCarrier.Id)
            {
                var updateShipmentCarrier = mapper.Map<ShipmentCarrier>(shipmentCarrier);
                context.ShipmentCarriers!.Update(updateShipmentCarrier);
                await context.SaveChangesAsync();
            }
        }
    }
}
