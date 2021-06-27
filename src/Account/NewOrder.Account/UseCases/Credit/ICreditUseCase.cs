using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public interface ICreditUseCase
    {
        Result Credit(long accountNumber, decimal amount);
    }
}
