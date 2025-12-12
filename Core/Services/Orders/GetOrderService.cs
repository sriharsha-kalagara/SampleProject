using BusinessEntities;
using Common;
using Data.Repositories;
using System;

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
    }
}
