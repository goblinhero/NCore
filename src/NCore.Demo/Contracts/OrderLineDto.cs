using NCore.Nancy.Contracts;

namespace NCore.Demo.Contracts
{
    public class OrderLineDto : EntityDto
    {
        public virtual long OrderId { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int? Index { get; set; }

    }
}