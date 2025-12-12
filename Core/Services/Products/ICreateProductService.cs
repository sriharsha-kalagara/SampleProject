using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
       Product Create(Guid id, string name, string description,
          decimal price, long quantity,
          DateTime availableFrom, DateTime availableTo, IEnumerable<string> Tags);
    }
}
