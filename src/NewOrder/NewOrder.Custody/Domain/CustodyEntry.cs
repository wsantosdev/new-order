using System.Runtime.InteropServices;

namespace NewOrder.Custody
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public readonly struct CustodyEntry
    {
        public readonly string Symbol;
        public readonly int Quantity;

        private CustodyEntry(string symbol, int quantity) =>
            (Symbol, Quantity) = (symbol, quantity);

        public static CustodyEntry Create(string symbol, int quantity) =>
            new CustodyEntry(symbol, quantity);
    }
}