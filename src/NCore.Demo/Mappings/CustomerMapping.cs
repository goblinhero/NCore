using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class CustomerMapping : ClassMapping<Customer>
    {
        public CustomerMapping()
        {
            this.MapEntity<CustomerMapping, Customer>();
            this.MapDemoCompany<CustomerMapping, Customer>();
            this.MapAddress<CustomerMapping, Customer>(c => c.Address);
            Property(m => m.CompanyName, m => m.NotNullable(true));
        }
    }
}