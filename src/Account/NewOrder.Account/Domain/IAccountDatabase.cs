namespace NewOrder.Account
{
    public interface IAccountDatabase
    {
        Account Get(long accountNumber);
        void Save(Account account);
    }
}
