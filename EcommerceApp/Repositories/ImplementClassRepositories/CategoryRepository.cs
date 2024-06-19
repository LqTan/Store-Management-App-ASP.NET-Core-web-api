using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public CategoryRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(CategoryModel category)
        {
            var newCategory = mapper.Map<Category>(category);
            context.Categories!.Add(newCategory);
            await context.SaveChangesAsync();
            return newCategory.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteCategory = context.Categories!.SingleOrDefault(c => c.Id == id);
            if (deleteCategory != null)
            {
                context.Categories!.Remove(deleteCategory);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryModel>> GetAllAsync()
        {
            var category = await context.Categories!.ToListAsync();
            return mapper.Map<List<CategoryModel>>(category);
        }

        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            var category = await context.Categories!.FindAsync(id);
            return mapper.Map<CategoryModel>(category);
        }

        public async Task UpdateAsync(int id, CategoryModel category)
        {
            if (id == category.Id)
            {
                var updateCategory = mapper.Map<Category>(category);
                context.Categories!.Update(updateCategory);
                await context.SaveChangesAsync();
            }
        }
    }
}
