namespace NCore.Demo.Domain
{
    public class Customer : Entity<Customer>
    {
        public virtual string CompanyName { get; set; }
        public virtual Address Address { get; set; }
    }
}