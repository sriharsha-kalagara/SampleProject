using BusinessEntities;
using System;
using System.Collections.Generic;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order Get(Guid orderId);

        IEnumerable<Order> GetByCustomerId(Guid id);
    }
}
