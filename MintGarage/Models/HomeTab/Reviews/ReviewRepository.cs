using MintGarage.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private MintGarageContext context;
        public ReviewRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Review> Reviews => context.Reviews;

        public void AddReviews(Review reviews)
        {
            context.Reviews.Add(reviews);
            SaveReviews();
        }

        public void DeleteReviews(Review reviews)
        {
            context.Reviews.Remove(reviews);
            SaveReviews();
        }

        public void UpdateReviews(Review reviews)
        {
            context.Reviews.Update(reviews);
            SaveReviews();
        }
        public void SaveReviews()
        {
            context.SaveChanges();
        }
    }
}
