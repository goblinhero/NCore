namespace NCore.Demo.Contracts
{
    public class InvoiceLineDto
    {
        public long InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int? Index { get; set; }

        protected bool Equals(InvoiceLineDto other)
        {
            return InvoiceId == other.InvoiceId && Index == other.Index;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((InvoiceLineDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (InvoiceId.GetHashCode()*397) ^ Index.GetHashCode();
            }
        }
    }
}