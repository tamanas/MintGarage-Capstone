using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.HomeT.Reviews
{
    public class ReviewRepository : IRepository<Review>
    {
        private MintGarageContext context;
        public ReviewRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Review> Items => context.Review;

        public void Create(Review item)
        {
            context.Review.Add(item);
            Save();
        }
        public void Update(Review item)
        {
            context.Review.Update(item);
            Save();
        }
        public void Delete(Review item)
        {
            context.Review.Remove(item);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
