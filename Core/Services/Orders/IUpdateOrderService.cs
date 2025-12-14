using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order,
            Guid customerId,
            Address address,
            IEnumerable<OrderItem> items,
            OrderStatus orderStatus);
    }
}
