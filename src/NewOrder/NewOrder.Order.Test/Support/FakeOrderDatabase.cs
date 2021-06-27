using System;
using System.Collections.Concurrent;

namespace NewOrder.Order.Test
{
    public class FakeOrderDatabase : IOrderDatabase
    {
        private readonly ConcurrentDictionary<Guid, Order> _storage = new();
        
        public void Save(Order order)
        {
            _storage.AddOrUpdate(order.Id,
                                 order,
                                 (accountNumber, currentOrder) => order);
        }
    }
}
