using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order
{
    public interface ICustodyService
    {
        ValueTask<Result> Add(long accountNumber, string symbol, int quantity);
        ValueTask<Result> Remove(long accountNumber, string symbol, int quantity);
    }
}
