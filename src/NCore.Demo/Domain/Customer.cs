namespace NCore.Demo.Domain
{
    public class Customer : Entity<Customer>
    {
        public Customer()
        {
            Address = Address.Blank;
        }

        public virtual string CompanyName { get; set; }
        public virtual Address Address { get; set; }
    }
}