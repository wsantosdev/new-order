namespace NewOrder.Order.WebApi
{
    public class SellRequest
    {
        public long AccountNumber { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}