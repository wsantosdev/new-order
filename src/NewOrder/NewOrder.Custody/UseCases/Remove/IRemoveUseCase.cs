using CSharpFunctionalExtensions;

namespace NewOrder.Custody
{
    public interface IRemoveUseCase
    {
        Result Remove(int accountNumber, CustodyEntry custodyEntry);
    }
}