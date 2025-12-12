using BusinessEntities;
using Common;
using Core.Factories;
using Data.Repositories;
using System;

namespace Core.Services.Products
{
    [AutoRegister]
    public class CreateProductService
        : ICreateProductService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Product> _productFactory;
        private readonly IProductsRepository _productsRepository;

        public CreateProductService(IIdObjectFactory<Product> productFactory,
            IProductsRepository productRepository, IUpdateProductService updateProductService)
        {
            _productFactory = productFactory;
            _productsRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Product Create(Guid id, string name, string description,
           decimal price, long quantity,
           DateTime availableFrom, DateTime availableTo)
        {
            var proudct = _productFactory.Create(id);
            _updateProductService.Update(proudct, name, description, price, quantity, availableFrom, availableTo);
            _productsRepository.Save(proudct);
            return proudct;
        }
    }
}
