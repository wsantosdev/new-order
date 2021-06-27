namespace NewOrder.Order
{
    public interface IOrderDatabase
    {
        void Save(Order order);
    }
}