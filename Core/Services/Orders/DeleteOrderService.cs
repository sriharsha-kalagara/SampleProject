using BusinessEntities;
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

        public void Delete(Order order)
        {
            order.Status = OrderStatus.Cancelled;

            order.CancelledAt = DateTime.UtcNow;

            _orderRepository.Delete(order);
        }
    }
}
