
using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class DebitUseCase : IDebitUseCase
    {
        private readonly IAccountDatabase _database;

        public DebitUseCase(IAccountDatabase database) =>
            _database = database;

        public Result Debit(long accountNumber, decimal amount)
        {
            var account = _database.Get(accountNumber);
            if (account is null)
                return Result.Failure($"Account {accountNumber} not found.");

            var debitResult = account.Debit(amount);
            if (debitResult.IsSuccess)
                _database.Save(account);

            return debitResult;
        }
    }
}
