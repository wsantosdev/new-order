using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class CreateUseCase : ICreateUseCase
    {
        private readonly IAccountDatabase _database;

        public CreateUseCase(IAccountDatabase database) =>
            _database = database;

        public Result Create(long accountNumber, decimal initialDeposit = 0)
        {
            var account = _database.Get(accountNumber);
            if (account != null)
                return Result.Failure($"The is already an account with number {accountNumber}.");

            var createResult = Account.Create(accountNumber, initialDeposit);
            if (createResult.IsFailure)
                return createResult;

            _database.Save(createResult.Value);
            return createResult;
        }
    }
}