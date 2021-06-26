using CSharpFunctionalExtensions;
using System.Collections.Generic;

namespace NewOrder.Custody
{
    public class Custody
    {
        protected Dictionary<string, int> Entries { private get; set; } = new Dictionary<string, int>();

        public int AccountNumber { get; protected set; }

        protected Custody() { }

        private Custody(int accountNumber) =>
            AccountNumber = accountNumber;

        public static Result<Custody> Create(int accountNumber)
        {
            if (accountNumber <= 0)
                return Result.Failure<Custody>("A positive account number must be provided.");

            return Result.Success(new Custody(accountNumber));
        }

        public int GetQuantity(string symbol)
        {
            if (!Entries.ContainsKey(symbol))
                return 0;

            return Entries[symbol];
        }

        public Result Add(string symbol, int quantity)
        {
            var validateResult = Validate(symbol, quantity);
            if (validateResult.IsFailure)
                return validateResult;

            if (!Entries.ContainsKey(symbol))
            {
                Entries.TryAdd(symbol, quantity);
                return Result.Success();
            }

            Entries[symbol] += quantity;
            return Result.Success();
        }

        public Result Remove(string symbol, int quantity)
        {
            var validateResult = Validate(symbol, quantity);
            if (validateResult.IsFailure)
                return validateResult;

            if (!Entries.ContainsKey(symbol))
                return Result.Failure($"Symbol {symbol} not found.");

            if (Entries[symbol] < quantity)
                return Result.Failure($"There is {Entries[symbol]} available stocks to sell only.");

            Entries[symbol] -= quantity;

            if (Entries[symbol] == 0)
                Entries.Remove(symbol);

            return Result.Success();
        }

        private Result Validate(string symbol, int quantity)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return Result.Failure("A symbol must be provided");

            if (quantity <= 0)
                return Result.Failure("A positive quantity must be provided");

            return Result.Success();
        }
    }
}
