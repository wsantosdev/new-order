using System.Collections.Concurrent;

namespace NewOrder.Account.Test
{
    public class FakeAccountDatabase : IAccountDatabase
    {
        private readonly ConcurrentDictionary<long, Account> _storage = new();
        
        public Account Get(long accountNumber)
        {
            return _storage.TryGetValue(accountNumber, out var account)
                ? account
                : null;
            
        }

        public void Save(Account account)
        {
            _storage.AddOrUpdate(account.Number,
                                 account,
                                 (accountNumber, currentAccount) => account);
        }
    }
}
