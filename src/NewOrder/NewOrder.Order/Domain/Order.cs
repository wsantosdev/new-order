using CSharpFunctionalExtensions;
using System;

namespace NewOrder.Order
{
    public class Order
    {
        public Guid Id { get; protected set; }
        public long AccountNumber { get; protected set; }
        public bool IsBuy { get; protected set; }
        public string Symbol { get; protected set; }
        public int Quantity { get; protected set; }
        public decimal Price { get; protected set; }

        protected Order() { }

        private Order(Guid id, long accountNumber, bool isBuy, string symbol, int quantity, decimal price) =>
            (Id, AccountNumber, IsBuy, Symbol, Quantity, Price) =
            (id, accountNumber, isBuy, symbol, quantity, price);

        public static Result<Order> Create(Guid id, long accountNumber, bool isBuy,
                                           string symbol, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(symbol))
                return Result.Failure<Order>("A valid symbol must be provided.");

            if (quantity <= 0)
                return Result.Failure<Order>("A positive quantity must be provided.");

            if (price <= 0)
                return Result.Failure<Order>("A positive price must be provided.");

            return Result.Success(new Order(id, accountNumber, isBuy, symbol, quantity, price));
        }
    }
}
