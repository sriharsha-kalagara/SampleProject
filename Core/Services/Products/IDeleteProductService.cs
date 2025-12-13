using System;

namespace Core.Services.Products
{
    public interface IDeleteProductService
    {
        void Delete(Guid productId);
    }
}
