using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MintGarage.Models.AboutUsTab.Values
{
    public interface IValueRepository
    {
        IQueryable<Value> Values { get; }
        void Update(Value value);
        void Save();
    }
}
