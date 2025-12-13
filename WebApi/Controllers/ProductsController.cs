using Core.Services.Products;
using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [RoutePrefix("products")]
    public class ProductsController : BaseApiController
    {
        // This is a placeholder for the ProductsController implementation.
        // Actual methods and dependencies would be defined here.

        private readonly ICreateProductService _createProductService;
        private readonly IDeleteProductService _deleteProductService;
        private readonly IGetProductService _getProductService;
        private readonly IUpdateProductService _updateProductService;

        public ProductsController(
            ICreateProductService createProductService,
            IDeleteProductService deleteProductService,
            IGetProductService getProductService,
            IUpdateProductService updateProductService)
        {
            _createProductService = createProductService;
            _deleteProductService = deleteProductService;
            _getProductService = getProductService;
            _updateProductService = updateProductService;
        }

        [Route("{productId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage Create(Guid productId, [FromBody] ProductModel model)
        {
            var IsProductExist = _getProductService.Get(productId); 

            if(IsProductExist != null)
               return Conflict($"PUT attempted on document 'products/{productId}' " +
                    "using a non current etag\" means that the record with the same ID already exists.");

            var product = _createProductService.Create(productId,
                model.Name, model.Description, model.Price, model.Stock, model.AvailableFrom,
                model.AvailableTo,
                model.Tags,
                model.GenderTags);

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage Update(Guid productId, [FromBody] ProductModel model)
        {
            var product = _getProductService.Get(productId);
            if (product == null)
            {
                return DoesNotExist();
            }

            _updateProductService.Update
                (product, model.Name, model.Description, model.Price, model.Stock, model.AvailableFrom,
                model.AvailableTo,
                model.Tags, model.GenderTags);

            return Found(new ProductData(product));
        }

        [Route("{productId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(Guid productId)
        {
            var product = _getProductService.Get(productId);
            if (product == null)
            {
                return DoesNotExist();
            }

            _deleteProductService.Delete(productId);
            return Found();
        }

        [Route("{productId:guid}")]
        [HttpGet]
        public HttpResponseMessage Get(Guid productId)
        {
            var product = _getProductService.Get(productId);
            return Found(new ProductData(product));
        }

        [Route("list")]
        [HttpGet]
        public HttpResponseMessage GetAll(int skip, int take)
        {
            var products = _getProductService.GetAll()
                .Skip(skip)
                .Take(take)
                .Select(u => new ProductData(u))
                .ToList();
            return Found(products);
        }
    }
}
