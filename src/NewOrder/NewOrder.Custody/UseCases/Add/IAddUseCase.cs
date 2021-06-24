using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface IAddUseCase
    {
        Result Add(int accountNumber, CustodyEntry custodyEntry);
    }
}