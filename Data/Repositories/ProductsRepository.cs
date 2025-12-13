using BusinessEntities;
using Common;
using Data.Indexes;
using Microsoft.Extensions.Caching.Memory;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister]
    public class ProductsRepository
        : Repository<Product>, IProductsRepository
    {
        private readonly IDocumentSession _documentSession;
        private readonly IMemoryCache _memoryCache;

        public ProductsRepository(
            IMemoryCache memoryCache,
            IDocumentSession documentSession)
            : base(documentSession)
        {
            _memoryCache = memoryCache;
            _documentSession = documentSession;
        }

        public void Delete(Product entity)
        {
            _documentSession.Delete(entity);

            _memoryCache.Remove("lendingtree_products");
        }

        public Product Get(Guid id)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

            query = query.WhereLessThanOrEqual("AvailableFrom", DateTime.UtcNow);
            query = query.AndAlso();
            query = query.WhereGreaterThanOrEqual("AvailableTo", DateTime.UtcNow);
            query = query.AndAlso();
            query = query.WhereGreaterThan("Stock", "0");
            query = query.AndAlso();
            query = query.WhereEquals("Id", "products/" + id);

            return query.FirstOrDefault();
        }

        public IEnumerable<Product> Get(string genderTag, string tag)
        {
            var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

            query = query.WhereLessThanOrEqual("AvailableFrom", DateTime.UtcNow);
            query = query.AndAlso();
            query = query.WhereGreaterThanOrEqual("AvailableTo", DateTime.UtcNow);
            query = query.AndAlso();
            query = query.WhereGreaterThan("Stock", "0");

            if (!string.IsNullOrWhiteSpace(tag))
            {
                query = query.AndAlso();

                query = query.ContainsAny("Tags", new string[] { tag });
            }

            if (!string.IsNullOrWhiteSpace(genderTag))
            {
                query = query.AndAlso();

                query = query.ContainsAny("GenderTags", new string[] { genderTag });
            }

            return query.ToList();
        }

        public List<Product> GetAll()
        {
            return _memoryCache.GetOrCreate("lendingtree_products", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(10);

                var query = _documentSession.Advanced.DocumentQuery<Product, ProductsListIndex>();

                query = query.WhereLessThanOrEqual("AvailableFrom", DateTime.UtcNow);
                query = query.AndAlso();
                query = query.WhereGreaterThanOrEqual("AvailableTo", DateTime.UtcNow);
                query = query.AndAlso();
                query = query.WhereGreaterThan("Stock", "0");

                return query.ToList();
            });
        }

        public void Save(Product product)
        {
            _documentSession.Store(product);

            _memoryCache.Remove("lendingtree_products");
        }

        public void Update(Product product)
        {
            _documentSession.Store(product);

            _memoryCache.Remove("lendingtree_products");
        }
    }
}
