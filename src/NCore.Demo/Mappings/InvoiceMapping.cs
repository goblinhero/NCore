using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class InvoiceMapping : ClassMapping<Invoice>
    {
        public InvoiceMapping()
        {
            this.MapTransaction<InvoiceMapping, Invoice>();
            this.MapDemoCompany<InvoiceMapping, Invoice>();
            this.MapAddress<InvoiceMapping, Invoice>(c => c.Address);
            this.MapDate<InvoiceMapping, Invoice>(m => m.Date);
            Property(m => m.Total);
            ManyToOne(m => m.Customer);
            Bag(m => m.Lines, cm =>
            {
                cm.Table("InvoiceLine");
                cm.Key(k => k.Column("InvoiceId"));
                cm.Cascade(Cascade.All);
                cm.Access(Accessor.Field);
            }, ce => ce.Component(c =>
            {
                c.Property(m => m.Description);
                c.Property(m => m.Total);
                c.Property(m => m.Index);
            }));
        }
    }
}