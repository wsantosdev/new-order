using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface ICreateUseCase
    {
        Result Create(int accountNumber);
    }
}