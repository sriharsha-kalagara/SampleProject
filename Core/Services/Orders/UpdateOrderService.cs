using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    [AutoRegister]
    public class UpdateOrderService
        : IUpdateOrderService
    {
        public void Update(Order order, 
            Guid customerId,
            Address address,
            IEnumerable<OrderItem> items)
        {
            order.SetCustomerId(customerId);
            order.SetOrderItems(items);
            order.SetShippingAddress(address);
            order.CalculateTotalAmount();
        }
    }
}
