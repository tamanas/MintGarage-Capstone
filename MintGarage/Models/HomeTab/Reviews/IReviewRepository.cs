using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.HomeTab.Reviews
{
    public interface IReviewRepository
    {
        IQueryable<Review> Reviews { get; }
        void AddReviews(Review reviews);
        void DeleteReviews(Review reviews);
        void UpdateReviews(Review reviews);
        void SaveReviews();
    }
}
