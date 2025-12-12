using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Orders
{
    public interface IUpdateOrderService
    {
        void Update(Order order,
            Guid customerId,
            Address address,
            IEnumerable<OrderItem> items);
    }
}
