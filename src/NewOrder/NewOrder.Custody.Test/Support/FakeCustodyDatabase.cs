using System.Collections.Concurrent;

namespace NewOrder.Custody.Test
{
    public class FakeCustodyDatabase : ICustodyDatabase
    {
        private readonly ConcurrentDictionary<long, Custody> _storage = new();

        public Custody Get(long accountNumber)
        {
            return _storage.TryGetValue(accountNumber, out var custody)
                ? custody
                : null;

        }

        public void Save(Custody custody)
        {
            _storage.AddOrUpdate(custody.AccountNumber,
                                 custody,
                                 (accountNumber, currentCustody) => custody);
        }
    }
}
