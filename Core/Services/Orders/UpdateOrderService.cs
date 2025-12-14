using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class UpdateOrderService
        : IUpdateOrderService
    {
        public void Update(Order order, 
            Guid customerId,
            Address address,
            IEnumerable<OrderItem> items,
            OrderStatus orderStatus)
        {
            order.SetCustomerId(customerId);
            order.SetOrderItems(items);
            order.SetShippingAddress(address);
            order.CalculateTotalAmount();
            order.UpdateStatsus(orderStatus);
        }
    }
}
