using Common;
using Data.Repositories;
using System;

namespace Core.Services.Products
{
    [AutoRegister]
    public class DeleteProductService : IDeleteProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IUpdateProductService _updateProductService;

        public DeleteProductService(IProductsRepository productsRepository, 
            IUpdateProductService updateProductService)
        {
            _productsRepository = productsRepository;
            _updateProductService = updateProductService;
        }

        public void Delete(Guid productId)
        {
            var product = _productsRepository.Get(productId) 
                ?? throw new ArgumentException("Product not found.");

            _updateProductService.Update(product, product.Name, product.Description,
                product.Price, product.Stock,
                DateTime.UtcNow.AddDays(-1), DateTime.UtcNow.AddDays(-1),
                product.Tags, product.GenderTags);

            _productsRepository.Delete(product);
        }
    }
}
