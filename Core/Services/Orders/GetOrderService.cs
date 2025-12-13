using BusinessEntities;
using Common;
using Data.Repositories;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class GetOrderService : IGetOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order Get(Guid orderId)
        {
            return _orderRepository.Get(orderId);
        }

        public IEnumerable<Order> GetByCustomerId(Guid id)
        {
            return _orderRepository.GetByCustomerId(id);
        }
    }
}
