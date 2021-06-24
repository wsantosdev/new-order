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
            if (accountNumber == 0)
                return Result.Failure<Custody>("A positive account number must be provided.");

            return Result.Success(new Custody(accountNumber));
        }

        public Result Add(CustodyEntry entry)
        {
            var validateResult = ValidateEntry(entry);
            if (validateResult.IsFailure)
                return validateResult;

            if (!Entries.ContainsKey(entry.Symbol))
            {
                Entries.TryAdd(entry.Symbol, entry.Quantity);
                return Result.Success();
            }

            Entries[entry.Symbol] += entry.Quantity;
            return Result.Success();
        }

        public Result Remove(CustodyEntry entry)
        {
            var validateResult = ValidateEntry(entry);
            if (validateResult.IsFailure)
                return validateResult;

            if (!Entries.ContainsKey(entry.Symbol))
                return Result.Failure($"Symbol {entry.Symbol} not found.");

            if (Entries[entry.Symbol] < entry.Quantity)
                return Result.Failure($"There is only {Entries[entry.Symbol]} available stocks to sell");

            Entries[entry.Symbol] -= entry.Quantity;

            if (Entries[entry.Symbol] == 0)
                Entries.Remove(entry.Symbol);

            return Result.Success();
        }

        private Result ValidateEntry(CustodyEntry entry)
        {
            if (string.IsNullOrWhiteSpace(entry.Symbol))
                return Result.Failure("A symbol must be provided");

            if (entry.Quantity <= 0)
                return Result.Failure("A positive quantity must be provided");

            return Result.Success();
        }
    }
}
