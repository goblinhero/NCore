using System.Linq;
using NCore.Rules;

namespace NCore.Demo.Domain
{
    public class Customer : Entity<Customer>,IHasCompany
    {
        protected Customer() { }
        public Customer(ICompany company)
        {
            Company = company;
            Address = Address.Blank;
        }

        public virtual string CompanyName { get; set; }
        public virtual Address Address { get; set; }
        protected override IRule<Customer>[] GetBusinessRules()
        {
            return new IRule<Customer>[]
            {
                new RelayRule<Customer>(c => string.IsNullOrWhiteSpace(c.CompanyName),"Customers must have a name."),
                new RelayRule<Customer>(c => c.Address == null,"Customers must have an address (can be blank)."),
                new RelayRule<Customer>(i => i.Company == null,"Customers must have a Company."),
            };
        }

        public virtual ICompany Company { get; protected set; }
    }
}