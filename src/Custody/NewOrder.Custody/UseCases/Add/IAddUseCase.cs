using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface IAddUseCase
    {
        Result Add(long accountNumber, string symbol, int quantity);
    }
}