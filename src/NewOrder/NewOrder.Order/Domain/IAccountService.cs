using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order
{
    public interface IAccountService
    {
        ValueTask<Result> Debit(long accountNumber, decimal amount);
        ValueTask<Result> Credit(long accountNumber, decimal amount);
    }
}
