using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface IRemoveUseCase
    {
        Result Remove(long accountNumber, string symbol, int quantity);
    }
}