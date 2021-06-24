using CSharpFunctionalExtensions;

namespace NewOrder.Account
{
    public interface ICreateUseCase
    {
        Result Create(long accountNumber, decimal initialDeposit);
    }
}
