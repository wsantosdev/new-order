using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface IQueryUseCase
    {
        Result<Custody> Query(long accountNumber);
    }
}
