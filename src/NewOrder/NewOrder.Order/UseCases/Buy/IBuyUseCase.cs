using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order
{
    public interface IBuyUseCase
    {
        ValueTask<Result> Buy(long accountNumber, string symbol, int quantity, decimal price);
    }
}