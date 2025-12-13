using System;
using System.Collections.Generic;

namespace WebApi.Models.Products
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public long Stock { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string ImageUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<string> GenderTags { get; set; }
    }
}