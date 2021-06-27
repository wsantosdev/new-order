using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public interface IQueryUseCase
    {
        Result<Account> Query(long accountNumber);
    }
}
