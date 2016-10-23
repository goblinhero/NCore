namespace NCore.Demo.Domain
{
    public class OrderLine : Entity<OrderLine>,IHasCompany
    {
        protected OrderLine()
        {
        }

        public OrderLine(Order order)
        {
            Order = order;
            Company = order.Company;
        }

        public virtual Order Order { get; protected set; }
        public virtual string Description { get; set; }
        public virtual decimal Total { get; set; }
        public virtual int Index { get; set; }
        public virtual ICompany Company { get; protected set; }
    }
}