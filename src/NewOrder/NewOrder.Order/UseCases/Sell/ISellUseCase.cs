using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order
{
    public interface ISellUseCase
    {
        ValueTask<Result> Sell(long accountNumber, string symbol, int quantity, decimal price);
    }
}