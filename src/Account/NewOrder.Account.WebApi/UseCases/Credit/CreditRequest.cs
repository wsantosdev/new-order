namespace NewOrder.Account.WebApi
{
    public class CreditRequest
    {
        public long AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
