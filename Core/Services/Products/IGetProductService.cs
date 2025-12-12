using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    public interface IGetProductService
    {
        Product GetUser(Guid id);

        IEnumerable<Product> GetAll();
    }
}