using BusinessEntities;
using Core.Services.Orders;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Orders;

namespace WebApi.Controllers
{
    [RoutePrefix("Orders")]
    public class OrdersController : BaseApiController
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;

        public OrdersController(
            ICreateOrderService createOrderService,
            IGetOrderService getOrderService,
            IUpdateOrderService updateOrderService)
        {
            _createOrderService = createOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
        }

        [Route("{orderId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage Create(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _createOrderService.Create(orderId,
                model.CustomerId,
                new Address(model.ShippingAddress.Street,
                    model.ShippingAddress.City,
                    model.ShippingAddress.State,
                    model.ShippingAddress.ZipCode,
                    model.ShippingAddress.Country)
                , model.Items.Select(t =>
                {
                    return new OrderItem(t.ProductId, t.Quantity, t.Price);
                }));

            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage Update(Guid orderId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.Get(orderId);

            if (order == null)
                return DoesNotExist();

            _updateOrderService.Update
                (order,
               order.CustomerId,
                               new Address(model.ShippingAddress.Street,
                    model.ShippingAddress.City,
                    model.ShippingAddress.State,
                    model.ShippingAddress.ZipCode,
                    model.ShippingAddress.Country),
                model.Items.Select(t =>
                {
                    return new OrderItem(t.ProductId, t.Quantity, t.Price);
                }), OrderStatus.Processing);

            return Found(new OrderData(order));
        }

        [Route("{orderId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid orderId)
        {
            var order = _getOrderService.Get(orderId);

            if (order == null)
                return DoesNotExist();

            _updateOrderService.Update
                 (order, order.CustomerId, order.ShippingAddress,
                    order.Items, OrderStatus.Cancelled);

            return Found();
        }

        [Route("{orderId:guid}")]
        [HttpGet]
        public HttpResponseMessage Get(Guid orderId)
        {
            var order = _getOrderService.Get(orderId);

            if (order == null)
            {
                return DoesNotExist();
            }

            return Found(new OrderData(order));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetAll(Guid customerId)
        {
            var orders = _getOrderService.GetByCustomerId(customerId)
                .Select(u => new OrderData(u))
                .ToList();

            if(orders.Count == 0) { return DoesNotExist(); }

            return Found(orders);
        }
    }
}
