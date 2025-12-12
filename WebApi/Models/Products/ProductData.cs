using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Models.Products
{
    public class ProductData
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Tags { get; set; }
        public bool IsAvailable { get; set; }

        public string ImageURl { get; set; }
        public string ThumbnailUrl { get; set; }

        public ProductData(BusinessEntities.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            Tags = product.Tags?.ToList() ?? new List<string>();
            IsAvailable = product.AvailableFrom >= DateTime.Now && product.AvailableTo <= DateTime.Now;
            ImageURl = product.Graphics?.FirstOrDefault()?.ImageUrl;
            ThumbnailUrl = product.Graphics?.FirstOrDefault()?.ThumbnailUrl;
        }
    }
}