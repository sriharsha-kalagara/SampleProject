using System;

namespace WebApi.Models.Orders
{
    public class OrderItemsModel
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}