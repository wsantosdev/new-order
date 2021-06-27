using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class QueryUseCase : IQueryUseCase
    {
        private readonly ICustodyDatabase _database;

        public QueryUseCase(ICustodyDatabase database)
        {
            _database = database;
        }

        public Result<Custody> Query(long accountNumber)
        {
            var custody = _database.Get(accountNumber);
            return custody != null
                    ? Result.Success(custody)
                    : Result.Failure<Custody>($"Custody for account number {accountNumber} not found");
        }
    }
}
