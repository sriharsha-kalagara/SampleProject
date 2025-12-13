using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models.Orders
{
    public class OrderItemData
    {
        public OrderItemData(BusinessEntities.OrderItem item)
        {
            ProductId = item.ProductId;
            Quantity = item.Quantity;
            Price = item.UnitPrice;
        }
        public Guid ProductId { get; set; }
        public long Quantity { get; set; }
        public decimal Price { get; set; }
    }
}