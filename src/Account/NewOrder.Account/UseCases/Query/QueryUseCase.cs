using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class QueryUseCase : IQueryUseCase
    {
        private readonly IAccountDatabase _database;

        public QueryUseCase(IAccountDatabase database)
        {
            _database = database;
        }

        public Result<Account> Query(long accountNumber)
        {
            var account = _database.Get(accountNumber);
            return (account != null)
                    ? Result.Success(account)
                    : Result.Failure<Account>($"Account {accountNumber} not found.");
        }
    }
}
