using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class RemoveUseCase : IRemoveUseCase
    {
        private readonly ICustodyDatabase _database;

        public RemoveUseCase(ICustodyDatabase database) =>
            _database = database;

        public Result Remove(long accountNumber, string symbol, int quantity)
        {
            var custody = _database.Get(accountNumber);
            if (custody is null)
                return Result.Failure($"Custody with account number {accountNumber} not found.");

            var removeResult = custody.Remove(symbol, quantity);
            if (removeResult.IsFailure)
                return removeResult;

            _database.Save(custody);
            return removeResult;
        }
    }
}
