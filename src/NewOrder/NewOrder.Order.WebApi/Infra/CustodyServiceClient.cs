using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace NewOrder.Order.WebApi
{
    public class CustodyServiceClient : ICustodyService
    {
        public ValueTask<Result> Add(long accountNumber, string symbol, int quantity)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<Result> Remove(long accountNumber, string symbol, int quantity)
        {
            throw new System.NotImplementedException();
        }
    }
}
