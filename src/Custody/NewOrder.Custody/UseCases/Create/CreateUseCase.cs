using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class CreateUseCase : ICreateUseCase
    {
        private readonly ICustodyDatabase _database;

        public CreateUseCase(ICustodyDatabase database) =>
            _database = database;

        public Result Create(int accountNumber)
        {
            var custody = _database.Get(accountNumber);
            if (custody != null)
                return Result.Failure($"Custody with account number {accountNumber} already exists.");

            var createResult = Custody.Create(accountNumber);
            if (createResult.IsFailure)
                return createResult;

            _database.Save(createResult.Value);
            return createResult;
        }
    }
}
