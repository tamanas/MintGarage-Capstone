using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models
{
    public interface IRepository<T>
    {
        IQueryable<T> Items { get; }
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        void Save();
    }
}
