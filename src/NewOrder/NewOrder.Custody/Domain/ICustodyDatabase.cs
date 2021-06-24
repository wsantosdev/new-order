namespace NewOrder.Custody
{
    public interface ICustodyDatabase
    {
        public Custody Get(int accountNumber);
        public void Save(Custody custody);
    }
}
