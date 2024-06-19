using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public ReturnRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ReturnModel @return)
        {
            var newReturn = mapper.Map<Return>(@return);
            context.Returns!.Add(newReturn);
            await context.SaveChangesAsync();
            return newReturn.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteReturn = context.Returns!.SingleOrDefault(x => x.Id == id);
            if (deleteReturn != null)
            {
                context.Returns!.Remove(deleteReturn);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ReturnModel>> GetAllAsync()
        {
            var @return = await context.Returns!.ToListAsync();
            return mapper.Map<List<ReturnModel>>(@return);
        }

        public async Task<ReturnModel> GetByIdAsync(int id)
        {
            var @return = await context.Returns!.FindAsync(id);
            return mapper.Map<ReturnModel>(@return);
        }

        public async Task UpdateAsync(int id, ReturnModel @return)
        {
            if (id == @return.Id)
            {
                var updateReturn = mapper.Map<Return>(@return);
                context.Returns!.Update(updateReturn);
                await context.SaveChangesAsync();
            }
        }
    }
}
