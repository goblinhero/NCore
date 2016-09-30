using NCore.Demo.Contracts;
using NCore.Demo.Extensions;
using NCore.Nancy.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class CustomerDtoMapping : ClassMapping<CustomerDto>
    {
        public CustomerDtoMapping()
        {
            this.MapEntityDto<CustomerDtoMapping, CustomerDto>();
            this.MapAddressDto<CustomerDtoMapping, CustomerDto>(c => c.Address);
            Property(m => m.CompanyName, m => m.NotNullable(true));
        }
    }
}