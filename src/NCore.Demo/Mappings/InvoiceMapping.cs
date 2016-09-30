using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.NHibernate.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class InvoiceMapping : ClassMapping<Invoice>
    {
        public InvoiceMapping()
        {
            Mutable(false);
            this.MapTransaction<InvoiceMapping, Invoice>();
            this.MapAddress<InvoiceMapping, Invoice>(c => c.Address);
            ManyToOne(m => m.Order);
            ManyToOne(m => m.Customer);
        }
    }
}