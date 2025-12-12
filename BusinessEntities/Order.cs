using System;
using System.Collections.Generic;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public List<OrderItem> Items { get; set; } 
            = new List<OrderItem>();

        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }

        public Address ShippingAddress { get; set; }

        public void CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Quantity * item.UnitPrice;
            }
            TotalAmount = total;
        }

    }

    public class OrderItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

   
    public enum OrderStatus
    {
        Pending,
        Processing,
        Completed,
        Cancelled
    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
