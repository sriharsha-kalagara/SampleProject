using BusinessEntities;
using Common;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class DeleteOrderService : IDeleteOrderService
    {
        private readonly IUpdateOrderService _updateOrderService;

        public DeleteOrderService(
            IUpdateOrderService updateOrderService)
        {
            _updateOrderService = updateOrderService;
        }

        public void Delete(Order order)
        {
            _updateOrderService.Update(order, order.CustomerId,
                order.ShippingAddress, order.Items, OrderStatus.Cancelled);
        }
    }
}
