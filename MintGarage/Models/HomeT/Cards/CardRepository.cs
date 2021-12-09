using MintGarage.Database;
using System.Linq;

namespace MintGarage.Models.HomeT.Cards
{
    public class CardRepository : IRepository<Card>
    {
        private MintGarageContext context;
        public CardRepository(MintGarageContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Card> Items => context.Card;

        public void Create(Card item)
        {
            context.Card.Add(item);
            Save();
        }

        public void Delete(Card item)
        {
            context.Card.Remove(item);
            Save();
        }

        public void Update(Card item)
        {
            context.Card.Update(item);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}

