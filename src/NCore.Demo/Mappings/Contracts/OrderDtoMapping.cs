using NCore.Demo.Contracts;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class OrderDtoMapping : ClassMapping<OrderDto>
    {
        public OrderDtoMapping()
        {
            this.MapEntityDto<OrderDtoMapping, OrderDto>();
            this.MapCompanyDto<OrderDtoMapping, OrderDto>();
            this.MapAddressDto<OrderDtoMapping, OrderDto>(c => c.Address);
            Property(m => m.CustomerId);
            Property(m => m.CustomerCompanyName);
        }
    }
}