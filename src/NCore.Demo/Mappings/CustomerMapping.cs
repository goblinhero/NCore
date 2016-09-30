using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class CustomerMapping : ClassMapping<Customer>
    {
        public CustomerMapping()
        {
            this.MapEntity<CustomerMapping, Customer>();
            this.MapAddress<CustomerMapping, Customer>(c => c.Address);
            Property(m => m.CompanyName, m => m.NotNullable(true));
        }
    }
}