using BusinessEntities;
using Common;
using Raven.Client;
using System;

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

            _documentSession.SaveChanges();
        }

        public Order Get(Guid id)
        {
            return _documentSession.Load<Order>(id);
        }

        public void Save(Order order)
        {
            _documentSession.Store(order);

            _documentSession.SaveChanges();
        }

        public void Update(Order order)
        {
            _documentSession.Store(order);

            _documentSession.SaveChanges();
        }
    }
}
