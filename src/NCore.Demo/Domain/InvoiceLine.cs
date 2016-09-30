namespace NCore.Demo.Domain
{
    public class InvoiceLine : IHasId
    {
        public virtual long? Id { get; protected set; }
    }
}