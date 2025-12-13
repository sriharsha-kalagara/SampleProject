using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models.Orders
{
    public class OrderData
    {
        public OrderData(BusinessEntities.Order order)
        {
            Id = order.Id;
            OrderId = order.OrderId;
            CustomerId = order.CustomerId;
            ShippingAddress = new AddressData(order.ShippingAddress);
            CreatedAt = order.CreatedAt;
            Items = order.Items.Select(i => new OrderItemData(i)).ToList();
            OrderStatus = order.Status.ToString();
            CancelledAt = order.CancelledAt;
            Total = order.TotalAmount;
        }

        public Guid Id { get; set; }
        public long OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public AddressData ShippingAddress { get; set; }
        public List<OrderItemData> Items { get; set; }
        public DateTime CreatedAt { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? CancelledAt { get; set; }
        public decimal Total { get; set; }
    }
}