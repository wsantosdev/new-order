using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class RemoveUseCase : IRemoveUseCase
    {
        private readonly ICustodyDatabase _database;

        public RemoveUseCase(ICustodyDatabase database) =>
            _database = database;

        public Result Remove(int accountNumber, CustodyEntry custodyEntry)
        {
            var custody = _database.Get(accountNumber);
            if (custody is null)
                return Result.Failure($"Custody not found to the account number {accountNumber}.");

            var removeResult = custody.Remove(custodyEntry);
            if (removeResult.IsSuccess)
                _database.Save(custody);

            return removeResult;
        }
    }
}
