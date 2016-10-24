using NCore.Rules;

namespace NCore.Demo.Domain
{
    public class Company : Entity<Company>, ICompany
    {
        public Company()
        {
            Address = Address.Blank;
        }
        public virtual string CompanyName { get; set; }
        public virtual Address Address { get; set; }
        protected override IRule<Company>[] GetBusinessRules()
        {
            return new IRule<Company>[]
            {
                new RelayRule<Company>(c => string.IsNullOrWhiteSpace(c.CompanyName),"Companies must have a name."),
                new RelayRule<Company>(c => c.Address == null,"Companies must have an address (can be blank)."),
            };
        }
    }
}