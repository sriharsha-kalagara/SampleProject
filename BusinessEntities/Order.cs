using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessEntities
{
    public class Order : IdObject
    {
        private long _orderId;
        private decimal _totalAmount;
        private Address _shippingAddress;
        private Guid _customerId;
        private List<OrderItem> _orderItems = new List<OrderItem>();

        public long OrderId {
            get => _orderId;
            private set => _orderId = value;
        }

        public decimal TotalAmount
        {
            get => _totalAmount;
            private set => _totalAmount = value;
        }
        
        public Guid CustomerId
        {
            get => _customerId;
            private set => _customerId = value;
        }

        public List<OrderItem> Items
        {
            get => _orderItems;
            private set => _orderItems = value;
        }

        public OrderStatus Status { get; set; }

        public DateTime? CancelledAt { get; set; }

        public Address ShippingAddress
        {
            get => _shippingAddress;
            private set => _shippingAddress = value;
        }

        public void GeneratOrderId()
        {
            _orderId = DateTime.UtcNow.Ticks;
        }

        public void CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Quantity * item.UnitPrice;
            }

            _totalAmount = total;
        }

        public void UpdateStatsus(OrderStatus newStatus)
        {
            Status = newStatus;

            if (newStatus == OrderStatus.Cancelled)
            {
                CancelledAt = DateTime.UtcNow;
            }

            if(newStatus != OrderStatus.Cancelled)
            {
                CancelledAt = null;

                UpdateDate = DateTime.UtcNow;
            }
        }

        public void SetShippingAddress(Address address)
        {
            _shippingAddress = address;
        }

        public void SetCustomerId(Guid customerId)
        {
            _customerId = customerId;
        }

        public void SetOrderItems(IEnumerable<OrderItem> items)
        {
            _orderItems = items.ToList();
        }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
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
        public Address(string street, string city, string state, string zipCode, string country
            )
        {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
    }
}
