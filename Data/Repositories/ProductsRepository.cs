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
            _documentSession.SaveChanges();

            _memoryCache.Remove("lendingtree_products");
        }

        public Product Get(Guid id)
        {
            var products = GetAll();
            return products.FirstOrDefault(p => p.Id == id);
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

                return query.ToList();
            });
        }

        public void Save(Product product)
        {
            _documentSession.Store(product);

            _documentSession.SaveChanges();

            _memoryCache.Remove("lendingtree_products");
        }

        public void Update(Product product)
        {
            _documentSession.Store(product);

            _documentSession.SaveChanges();

            _memoryCache.Remove("lendingtree_products");
        }
    }
}
