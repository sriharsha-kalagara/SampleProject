using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product Get(Guid id);

        IEnumerable<Product> GetAll();

        IEnumerable<Product> Get(string genderTag = null, string tag = null);
    }
}