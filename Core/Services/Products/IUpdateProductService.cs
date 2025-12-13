using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IUpdateProductService
    {
        void Update(Product product, string name, string description,
           decimal price, long quantity,
           DateTime availableFrom, DateTime availableTo, IEnumerable<string> Tags,
           IEnumerable<string> GenderTags);
    }
}
