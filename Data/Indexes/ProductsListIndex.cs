using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System.Linq;

namespace Data.Indexes
{
    public class ProductsListIndex : AbstractIndexCreationTask<Product>
    {
        public ProductsListIndex()
        {
            Map = products => from product in products
                              select new
                           {
                               product.Name,
                               product.AvailableFrom,
                               product.AvailableTo,
                               product.Price,
                               product.Tags,
                               product.GenderTags
                           };

            Index(x => x.Description, FieldIndexing.NotAnalyzed);
        }
    }
}
