using LiteDB;

namespace NewOrder.Account.WebApi
{
    public class AccountDatabase : IAccountDatabase
    {
        private readonly string _connectionString;

        public AccountDatabase(string connectionString)
        {
            _connectionString = connectionString;
            BsonMapper.Global
                      .Entity<Account>()
                      .Id(a => a.Number);
        }
        
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
