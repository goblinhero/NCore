using NCore.Demo.Contracts;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class CustomerDtoMapping : ClassMapping<CustomerDto>
    {
        public CustomerDtoMapping()
        {
            Table("Customer");
            this.MapEntityDto<CustomerDtoMapping, CustomerDto>();
            this.MapAddressDto<CustomerDtoMapping, CustomerDto>(c => c.Address);
            Property(m => m.CompanyName);
        }
    }
}