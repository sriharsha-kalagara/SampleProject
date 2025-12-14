using BusinessEntities;
using Common;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    [AutoRegister]
    public class OrderRepository
       : Repository<Order>, IOrderRepository
    {
        private readonly IDocumentSession _documentSession;

        public OrderRepository
            (IDocumentSession documentSession)
            : base(documentSession)
        {
            _documentSession = documentSession;
        }

        public void Delete(Order order)
        {
            _documentSession.Store(order);
        }

        public Order Get(Guid id)
        {
            return _documentSession.Load<Order>(id);
        }

        public IEnumerable<Order> GetByCustomerId(Guid id)
        {
            var query = _documentSession.Advanced.DocumentQuery<Order>();

            query = query.WhereEquals("CustomerId", id);

            return query.ToList();
        }

        public void Save(Order order)
        {
            _documentSession.Store(order);
        }
    }
}
