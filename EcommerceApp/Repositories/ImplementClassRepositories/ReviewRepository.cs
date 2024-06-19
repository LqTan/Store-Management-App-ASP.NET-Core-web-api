using AutoMapper;
using EcommerceApp.Data.Context;
using EcommerceApp.Data.Entities;
using EcommerceApp.Models;
using EcommerceApp.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Repositories.ImplementClassRepositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly StoreContext context;
        private readonly IMapper mapper;

        public ReviewRepository(StoreContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<int> AddAsync(ReviewModel review)
        {
            var newReview = mapper.Map<Review>(review);
            context.Reviews!.Add(newReview);
            await context.SaveChangesAsync();
            return newReview.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var deleteReview = context.Reviews!.SingleOrDefault(review => review.Id == id);
            if (deleteReview != null)
            {
                context.Reviews!.Remove(deleteReview);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ReviewModel>> GetAllAsync()
        {
            var review = await context.Reviews!.ToListAsync();
            return mapper.Map<List<ReviewModel>>(review);
        }

        public async Task<ReviewModel> GetByIdAsync(int id)
        {
            var review = await context.Reviews!.FindAsync(id);
            return mapper.Map<ReviewModel>(review);
        }

        public async Task UpdateAsync(int id, ReviewModel review)
        {
            if (id == review.Id)
            {
                var updateReview = mapper.Map<Review>(review);
                context.Reviews!.Update(updateReview);
                await context.SaveChangesAsync();
            }
        }
    }
}
