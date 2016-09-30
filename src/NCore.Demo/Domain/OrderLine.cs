namespace NCore.Demo.Domain
{
    public class OrderLine : Entity<OrderLine>
    {
        public virtual Order Order { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int Index { get; set; }
    }
}