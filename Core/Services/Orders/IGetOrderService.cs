using BusinessEntities;
using System;

namespace Core.Services.Orders
{
    public interface IGetOrderService
    {
        Order Get(Guid orderId);
    }
}
