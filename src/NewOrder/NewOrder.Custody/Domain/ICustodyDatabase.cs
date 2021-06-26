namespace NewOrder.Custody
{
    public interface ICustodyDatabase
    {
        public Custody Get(long accountNumber);
        public void Save(Custody custody);
    }
}
