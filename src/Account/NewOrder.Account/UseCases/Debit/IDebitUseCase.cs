using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public interface IDebitUseCase
    {
        Result Debit(long accountNumber, decimal amount);
    }
}
