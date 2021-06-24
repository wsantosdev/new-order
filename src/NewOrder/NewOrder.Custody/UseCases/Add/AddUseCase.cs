using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public class AddUseCase : IAddUseCase
    {
        private readonly ICustodyDatabase _custodyDatabase;

        public AddUseCase(ICustodyDatabase database) =>
            _custodyDatabase = database;

        public Result Add(int accountNumber, CustodyEntry custodyEntry)
        {
            var custody = _custodyDatabase.Get(accountNumber);
            if (custody is null)
                return Result.Failure($"Custody not found for account number {accountNumber}.");

            var addResult = custody.Add(custodyEntry);
            if (addResult.IsSuccess)
                _custodyDatabase.Save(custody);

            return addResult;
        }
    }
}
