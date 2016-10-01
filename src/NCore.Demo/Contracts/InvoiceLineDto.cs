namespace NCore.Demo.Contracts
{
    public class InvoiceLineDto
    {
        public long InvoiceId { get; set; }
        public string Description { get; set; }
        public decimal Total { get; set; }
        public int? Index { get; set; }
    }
}