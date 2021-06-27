namespace NewOrder.Custody.WebApi
{
    public class RemoveRequest
    {
        public long AccountNumber { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
    }
}
