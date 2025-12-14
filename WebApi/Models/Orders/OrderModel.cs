using System;
using System.Collections.Generic;

namespace WebApi.Models.Orders
{
    public class OrderModel
    {
        public AddressModel ShippingAddress { get; set; }
        public List<OrderItemsModel> Items { get; set; }
    }
}