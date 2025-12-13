using BusinessEntities;
using Common;
using System;
using System.Collections.Generic;

namespace Core.Services.Products
{
    [AutoRegister(AutoRegisterTypes.Singleton)]
    public class UpdateProductService : IUpdateProductService
    {
        public void Update(Product product, string name, string description, 
            decimal price, long quantity, 
            DateTime availableFrom, DateTime availableTo, IEnumerable<string> Tags,
            IEnumerable<string> GenderTags
            )
        {
            product.SetName(name);
            product.SetDescription(description);
            product.SetPrice(price);
            product.SetStock(quantity);
            product.SetAvailability (availableFrom, availableTo);
            product.SetTags(Tags);
            product.SetGenderTags(GenderTags);  
        }
    }
}
