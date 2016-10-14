namespace NCore.Demo.Domain
{
    public class OrderLine : Entity<OrderLine>
    {
        protected OrderLine()
        {
        }

        public OrderLine(Order order)
        {
            Order = order;
        }

        public virtual Order Order { get; protected set; }
        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int Index { get; set; }
    }
}