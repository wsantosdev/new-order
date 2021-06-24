using CSharpFunctionalExtensions;
using System;
using System.Threading.Tasks;

namespace NewOrder.Order
{
    public class BuyUseCase : IBuyUseCase
    {
        private readonly IAccountService _accountService;
        private readonly ICustodyService _custodyService;
        private readonly IOrderDatabase _database;

        public BuyUseCase(IAccountService accountService,
                          ICustodyService custodyService,
                          IOrderDatabase database) =>
            (_accountService, _custodyService, _database)
            = (accountService, custodyService, database);

        public async ValueTask<Result> Buy(long accountNumber, string symbol, int quantity, decimal price)
        {
            var createResult = Order.Create(accountNumber, Guid.NewGuid(), true, symbol, quantity, price);
            if (createResult.IsFailure)
                return createResult;

            var debitResult = await _accountService.Debit(accountNumber, quantity * price).ConfigureAwait(false);
            if (debitResult.IsFailure)
                return debitResult;

            await _custodyService.Add(accountNumber, symbol, quantity).ConfigureAwait(false);
            _database.Save(createResult.Value);

            return createResult;
        }
    }
}
