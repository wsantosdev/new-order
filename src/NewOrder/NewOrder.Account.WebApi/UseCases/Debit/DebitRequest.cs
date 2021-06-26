namespace NewOrder.Account.WebApi
{
    public class DebitRequest
    {
        public long AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
