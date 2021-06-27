namespace NewOrder.Order.WebApi.Controllers
{
    public class BuyRequest
    {
        public long AccountNumber { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}