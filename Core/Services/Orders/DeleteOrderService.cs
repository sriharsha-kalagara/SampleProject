using Common;
using Data.Repositories;
using System;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void Delete(Guid orderId)
        {
            var order = _orderRepository.Get(orderId)
                ?? throw new ArgumentException("Order not found.");

            order.Status = BusinessEntities.OrderStatus.Cancelled;

            order.CancelledAt = DateTime.UtcNow;

            _orderRepository.Delete(order);
        }
    }
}
