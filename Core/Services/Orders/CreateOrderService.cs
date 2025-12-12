using BusinessEntities;
using Core.Factories;
using Core.Services.Products;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public class CreateOrderService
    {
        private readonly IUpdateProductService _updateProductService;
        private readonly IIdObjectFactory<Order> _orderFactory;
        private readonly IProductsRepository _productsRepository;

        public CreateOrderService(IIdObjectFactory<Order> productFactory,
            IProductsRepository productRepository, IUpdateProductService updateProductService)
        {
            _orderFactory = productFactory;
            _productsRepository = productRepository;
            _updateProductService = updateProductService;
        }

        public Order Create(Guid id, string name, string description,
           decimal price, long quantity,
           DateTime availableFrom, DateTime availableTo, IEnumerable<string> tags)
        {
            var order = _orderFactory.Create(id);
            order.GeneratOrderId();

            _updateProductService.Update
                (order, name, description, price, quantity, availableFrom, availableTo, tags);
            _productsRepository.Save(order);
            return order;
        }
    }
}
