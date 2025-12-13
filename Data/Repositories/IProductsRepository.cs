using BusinessEntities;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IProductsRepository
        : IRepository<Product>
    {
        List<Product> GetAll();

        void Update(Product product);

        IEnumerable<Product> Get(string genderTag, string tag);
    }
}
