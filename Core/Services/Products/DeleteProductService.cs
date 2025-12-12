using Common;
using Data.Repositories;
using System;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductsRepository _productsRepository;
        public DeleteProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public void Delete(Guid productId)
        {
            var product = _productsRepository.Get(productId) 
                ?? throw new ArgumentException("Product not found.");

            _productsRepository.Delete(product);
        }
    }
}
