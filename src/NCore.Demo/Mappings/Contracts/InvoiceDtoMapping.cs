using NCore.Demo.Contracts;
using NCore.Demo.Extensions;
using NCore.Nancy.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class InvoiceDtoMapping : ClassMapping<InvoiceDto>
    {
        public InvoiceDtoMapping()
        {
            this.MapEntityDto<InvoiceDtoMapping, InvoiceDto>();
            this.MapAddressDto<InvoiceDtoMapping, InvoiceDto>(c => c.Address);
            Property(m => m.CustomerId);
            Property(m => m.CustomerCompanyName);
            Property(m => m.DateDays);
        }
    }
}