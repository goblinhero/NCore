namespace NCore.Demo.Domain
{
    public class Customer : Entity<Customer>
    {
        public Customer()
        {
            Address = new Address(string.Empty,string.Empty,string.Empty);
        }
        public virtual string CompanyName { get; set; }
        public virtual Address Address { get; set; }
    }
}