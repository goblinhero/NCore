namespace NCore.Demo.Domain
{
    public class InvoiceLine : IValueType
    {
        protected InvoiceLine()
        {
        }

        public InvoiceLine(string description, decimal total, int index)
        {
            Description = description;
            Total = total;
            Index = index;
        }

        public string Description { get; private set; }
        public decimal Total { get; private set; }
        public int Index { get; private set; }
    }
}