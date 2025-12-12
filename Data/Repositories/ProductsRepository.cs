using BusinessEntities;
using Common;
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
        public ProductsRepository(IDocumentSession documentSession)
            : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public void Delete(Product entity)
        {
            _documentSession.Delete(entity);
            _documentSession.SaveChanges();
        }

        public Product Get(Guid id)
        {
            return _documentSession.Load<Product>(id);
        }

        public List<Product> GetAll()
        {
            return _documentSession.Load<Product>().ToList();
        }

        public void Save(Product product)
        {
            _documentSession.Store(product);

            _documentSession.SaveChanges();
        }

        public void Update(Product product)
        {
            _documentSession.Store(product);

            _documentSession.SaveChanges();
        }
    }
}
