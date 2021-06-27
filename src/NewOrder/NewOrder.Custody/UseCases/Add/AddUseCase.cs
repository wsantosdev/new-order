using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class AddUseCase : IAddUseCase
    {
        private readonly ICustodyDatabase _database;

        public AddUseCase(ICustodyDatabase database) =>
            _database = database;

        public Result Add(long accountNumber, string symbol, int quantity)
        {
            var custody = _database.Get(accountNumber);
            if (custody is null)
                return Result.Failure($"Custody with account number {accountNumber} not found.");

            var addResult = custody.Add(symbol, quantity);
            if (addResult.IsFailure)
                return addResult;

            _database.Save(custody);
            return addResult;
        }
    }
}
