using CSharpFunctionalExtensions;
using System;

namespace NewOrder.Order
{
    public class Order
    {
        public long AccountNumber { get; protected set; }
        public Guid OrderId { get; protected set; }
        public bool IsBuy { get; protected set; }
        public string Symbol { get; protected set; }
        public int Quantity { get; protected set; }
        public decimal Price { get; protected set; }

        protected Order() { }

        private Order(long accountNumber, Guid orderId, bool isBuy, string symbol, int quantity, decimal price) =>
            (AccountNumber, OrderId, IsBuy, Symbol, Quantity, Price) =
            (accountNumber, orderId, isBuy, symbol, quantity, price);

        public static Result<Order> Create(long accountNumber, Guid orderId, bool isBuy,
                                           string symbol, int quantity, decimal price)
        {
            if (orderId == Guid.Empty)
                return Result.Failure<Order>("A valid Guid must be provided to identify the order.");

            if (string.IsNullOrWhiteSpace(symbol))
                return Result.Failure<Order>("A valid symbol must be provided.");

            if (quantity <= 0)
                return Result.Failure<Order>("A positive quantity must be provided.");

            if (price <= 0)
                return Result.Failure<Order>("A positive price must be provided");

            return Result.Success(new Order(accountNumber, orderId, isBuy, symbol, quantity, price));
        }
    }
}
