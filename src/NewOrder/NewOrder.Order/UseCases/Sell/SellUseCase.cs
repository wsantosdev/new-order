using CSharpFunctionalExtensions;
using System;
using System.Threading.Tasks;

namespace NewOrder.Order.UseCases.Sell
{
    public class SellUseCase : ISellUseCase
    {
        private readonly ICustodyService _custodyService;
        private readonly IAccountService _accountService;
        private readonly IOrderDatabase _database;

        public SellUseCase(ICustodyService custodyService,
                           IAccountService accountService,
                           IOrderDatabase database) =>
            (_custodyService, _accountService, _database)
            = (custodyService, accountService, database);

        public async ValueTask<Result> Sell(long accountNumber, string symbol, int quantity, decimal price)
        {
            var createResult = Order.Create(accountNumber, Guid.NewGuid(), false, symbol, quantity, price);
            if (createResult.IsFailure)
                return createResult;
            
            var removeResult = await _custodyService.Remove(accountNumber, symbol, quantity).ConfigureAwait(false);
            if (removeResult.IsFailure)
                return removeResult;

            await _accountService.Credit(accountNumber, quantity * price).ConfigureAwait(false);
            _database.Save(createResult.Value);
            
            return createResult;
        }
    }
}
