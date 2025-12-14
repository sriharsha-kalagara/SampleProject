using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product Get(Guid id);

        IEnumerable<Product> GetAll();
        IEnumerable<Product> GetListOfProducts(IEnumerable<Guid> ids);
        IEnumerable<Product> Get(string genderTag = null, string tag = null);
    }
}