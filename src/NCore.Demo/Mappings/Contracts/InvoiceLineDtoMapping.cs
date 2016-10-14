using NCore.Demo.Contracts;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class InvoiceLineDtoMapping : ClassMapping<InvoiceLineDto>
    {
        public InvoiceLineDtoMapping()
        {
            Table("InvoiceLine");
            ComposedId(cid =>
            {
                cid.Property(m => m.InvoiceId);
                cid.Property(m => m.Index);
            });
            Property(m => m.Description);
            Property(m => m.InvoiceId);
            Property(m => m.Total);
            Property(m => m.Index, m => m.Column("[Index]"));
        }
    }
}