using BusinessEntities;
using Core.Services.Orders;
using Core.Services.Products;
using Core.Services.Users;
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
        private readonly IGetUserService _getUserService;
        private readonly ICreateOrderService _createOrderService;
        private readonly IGetOrderService _getOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IGetProductService _getProductService;

        public OrdersController(
            ICreateOrderService createOrderService,
            IGetOrderService getOrderService,
            IUpdateOrderService updateOrderService,
            IGetUserService getUserService,
            IGetProductService getProductService)
        {
            _createOrderService = createOrderService;
            _getOrderService = getOrderService;
            _updateOrderService = updateOrderService;
            _getUserService = getUserService;
            _getProductService = getProductService;
        }

        [Route("{orderId:guid}/{customerId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage Create(Guid orderId, Guid customerId, [FromBody] OrderModel model)
        {

            var isOrderExist = _getOrderService.Get(orderId);

            if(isOrderExist != null)
                return Conflict($"PUT attempted on document 'Orders/{orderId}' " +
                  "using a non current etag\" means that the record with the same ID already exists.");

            var userInfo = _getUserService.GetUser(customerId);

            if(userInfo == null)
                return DoesNotExist("Customer associated with the order not exist.");

            var products = _getProductService.GetListOfProducts(model.Items.Select(t => t.ProductId));

            if (products == null || products.Count() == 0
                || !model.Items.All(g => products.Any(t => t.Id == g.ProductId)))
                return DoesNotExist("Select products not exist.");

            var order = _createOrderService.Create(orderId,
                customerId,
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

        [Route("{orderId:guid}/{customerId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage Update(Guid orderId, Guid customerId, [FromBody] OrderModel model)
        {
            var order = _getOrderService.Get(orderId);

            if (order == null)
                return DoesNotExist();

            var userInfo = _getUserService.GetUser(customerId);

            if (userInfo == null)
                return DoesNotExist("Customer associated with the order not exist.");

            _updateOrderService.Update
                (order, customerId,
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

        [Route("{orderId:guid}/{customerId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid orderId, Guid customerId)
        {
            var order = _getOrderService.Get(orderId);

            if (order == null)
                return DoesNotExist();

            var userInfo = _getUserService.GetUser(customerId);

            if (userInfo == null)
                return DoesNotExist("Customer associated with the order not exist.");

            _updateOrderService.Update
                 (order, customerId, order.ShippingAddress,
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

        [Route("{customerId:guid}/list")]
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
