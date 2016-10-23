using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class CompanyMapping : ClassMapping<Company>
    {
        public CompanyMapping()
        {
            this.MapEntity<CompanyMapping, Company>();
            this.MapAddress<CompanyMapping, Company>(c => c.Address);
            Property(m => m.CompanyName, m => m.NotNullable(true));
        }
    }
}