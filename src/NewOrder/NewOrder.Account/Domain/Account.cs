using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public class Account
    {
        public long Number { get; }
        public decimal Balance { get; private set; }

        protected Account() { }

        private Account(long number, decimal initialDeposit = 0) =>
            (Number, Balance) = (number, initialDeposit);

        public static Result<Account> Create(long accountNumber, decimal initialDeposit = 0)
        {
            if (accountNumber == 0)
                return Result.Failure<Account>("A positive account number must be provided.");

            return Result.Success(new Account(accountNumber, initialDeposit));
        }

        public Result Debit(decimal amount)
        {
            if (amount == 0)
                return Result.Failure("A positive amount must be provided.");

            if (amount > Balance)
                return Result.Failure("Not enough funds to perform this operation.");

            Balance -= amount;
            return Result.Success();
        }

        public Result Credit(decimal amount)
        {
            if(amount == 0)
                return Result.Failure("A positive amount must be provided.");

            Balance += amount;
            return Result.Success();
        }
    }
}
