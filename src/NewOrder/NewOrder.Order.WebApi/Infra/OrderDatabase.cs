using LiteDB;

namespace NewOrder.Order.WebApi
{
    public class OrderDatabase : IOrderDatabase
    {
        private readonly string _connectionString;

        public OrderDatabase(string connectionString) =>
            _connectionString = connectionString;

        public void Save(Order order)
        {
            using var db = CreateDatabase();
            db.GetCollection<Order>()
              .Upsert(order);
        }

        private LiteDatabase CreateDatabase() =>
            new LiteDatabase(_connectionString);
    }
}
