using BusinessEntities;
using Common;
using Data.Repositories;
using Raven.Abstractions.Data;
using System;
using System.Collections.Generic;

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

        public IEnumerable<Product> Get(string genderTag = null, string tag = null)
        {
            return _productsRepository.Get(genderTag, tag);
        }

        public IEnumerable<Product> GetListOfProducts(IEnumerable<Guid> ids)
        {
            return _productsRepository.GetListOfProducts(ids);
        }
    }
}
