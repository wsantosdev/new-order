using LiteDB;

namespace NewOrder.Custody.WebApi
{
    public class CustodyDatabase : ICustodyDatabase
    {
        private readonly string _connectionString;

        public CustodyDatabase(string connectionString)
        {
            _connectionString = connectionString;

            BsonMapper.Global
                      .Entity<Custody>()
                      .Id(c => c.AccountNumber);
            BsonMapper.Global.IncludeNonPublic = true;
        }
        public Custody Get(long accountNumber)
        {
            using var db = CreateDatabase();
            return db.GetCollection<Custody>()
                     .FindOne(custody => custody.AccountNumber == accountNumber);
        }

        public void Save(Custody custody)
        {
            using var db = CreateDatabase();
            db.GetCollection<Custody>()
              .Upsert(custody);
        }

        private LiteDatabase CreateDatabase() =>
            new (_connectionString);
    }
}
