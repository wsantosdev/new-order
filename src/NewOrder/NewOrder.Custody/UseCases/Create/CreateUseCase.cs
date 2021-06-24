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
                return Result.Failure($"Custody not found to the account number {accountNumber}.");

            custody = Custody.Create(accountNumber);
            _database.Save(custody);

            return Result.Success();
        }
    }
}
