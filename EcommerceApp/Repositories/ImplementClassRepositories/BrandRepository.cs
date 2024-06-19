using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public BrandRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(BrandModel brand)
        {
            var newBrand = mapper.Map<Brand>(brand);
            context.Brands!.Add(newBrand);
            await context.SaveChangesAsync();
            return newBrand.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteBrand = context.Brands!.SingleOrDefault(b => b.Id == id);
            if (deleteBrand != null)
            {
                context.Brands!.Remove(deleteBrand);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<BrandModel>> GetAllAsync()
        {
            var brand = await context.Brands!.ToListAsync();
            return mapper.Map<List<BrandModel>>(brand);
        }

        public async Task<BrandModel> GetByIdAsync(int id)
        {
            var brand = await context.Brands!.FindAsync(id);
            return mapper.Map<BrandModel>(brand);
        }

        public async Task UpdateAsync(int id, BrandModel brand)
        {            
            if (id == brand.Id)
            {
                var updateBrand = mapper.Map<Brand>(brand);
                context.Brands!.Update(updateBrand);
                await context.SaveChangesAsync();
            }
        }
    }
}
