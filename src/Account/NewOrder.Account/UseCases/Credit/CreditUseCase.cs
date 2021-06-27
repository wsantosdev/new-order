using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class CreditUseCase : ICreditUseCase
    {
        private readonly IAccountDatabase _database;

        public CreditUseCase(IAccountDatabase database) =>
            _database = database;

        public Result Credit(long accountNumber, decimal amount)
        {
            var account = _database.Get(accountNumber);
            if (account is null)
                return Result.Failure($"Account {accountNumber} not found.");

            var creditResult = account.Credit(amount);
            if (creditResult.IsSuccess)
                _database.Save(account);

            return creditResult;
        }
    }
}
