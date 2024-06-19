using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public ProductRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ProductModel product)
        {
            var newProduct = mapper.Map<Product>(product);
            context.Products!.Add(newProduct);
            await context.SaveChangesAsync();
            return newProduct.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteProduct = context.Products!.SingleOrDefault(p => p.Id == id);
            if (deleteProduct != null)
            {
                context.Products!.Remove(deleteProduct);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductModel>> GetAllAsync()
        {
            var product = await context.Products!.ToListAsync();
            return mapper.Map<List<ProductModel>>(product);
        }

        public async Task<ProductModel> GetByIdAsync(int id)
        {
            var product = await context.Products!.FindAsync(id);
            return mapper.Map<ProductModel>(product);
        }

        public void Detach(Product product)
        {
            var entity = context.Entry(product);
            if (entity != null)
            {
                entity.State = EntityState.Detached;
            }
        }

        public async Task UpdateAsync(int id, ProductModel product)
        {
            if (id == product.Id)
            {
                var existingProduct = await context.Products!.FindAsync(id);
                if (existingProduct != null)
                {
                    Detach(existingProduct);

                    var updateProduct = mapper.Map<Product>(product);
                    context.Products!.Update(updateProduct);
                    await context.SaveChangesAsync();
                }                
            }
        }
    }
}
