using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Products
{
    [AutoRegister]
    public class GetProductService : IGetProductService
    {
        private readonly IProductsRepository _productsRepository;
        public GetProductService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        IEnumerable<Product> IGetProductService.GetAll()
        {
            return _productsRepository.GetAll();
        }

        Product IGetProductService.Get(Guid id)
        {
            return _productsRepository.Get(id);
        }
    }
}
