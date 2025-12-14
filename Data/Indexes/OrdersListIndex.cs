using BusinessEntities;
using Raven.Client.Indexes;
using System.Linq;

namespace Data.Indexes
{
    public class OrdersListIndex : AbstractIndexCreationTask<Order>
    {
        public OrdersListIndex()
        {
            Map = orders => from order in orders
                            select new
                            {
                                order.OrderId,
                                order.CustomerId,
                                order.Status,
                                order.Id
                            };
        }
    }
}
