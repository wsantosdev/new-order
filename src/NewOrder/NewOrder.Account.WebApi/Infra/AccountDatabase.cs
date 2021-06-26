using LiteDB;

namespace NewOrder.Account.WebApi
{
    public class AccountDatabase : IAccountDatabase
    {
        private readonly string _connectionString;

        public AccountDatabase(string connectionString) =>
            _connectionString = connectionString;
        
        public Account Get(long accountNumber)
        {
            using var db = CreateDatabase();
            return db.GetCollection<Account>()
                     .FindOne(account => account.Number == accountNumber);
        }

        public void Save(Account account)
        {
            using var db = CreateDatabase();
            db.GetCollection<Account>()
              .Upsert(account);
        }

        private LiteDatabase CreateDatabase() =>
            new (_connectionString);
    }
}
