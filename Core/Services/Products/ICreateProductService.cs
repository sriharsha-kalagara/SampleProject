using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface ICreateProductService
    {
       Product Create(Guid id, string name, string description,
          decimal price, long quantity,
          DateTime availableFrom, DateTime availableTo);
    }
}
