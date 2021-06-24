using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi
{
    public class AccountServiceClient : IAccountService
    {
        public ValueTask<Result> Credit(long accountNumber, decimal amount)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Result> Debit(long accountNumber, decimal amount)
        {
            throw new System.NotImplementedException();
        }
    }
}
