using NCore.Demo.Contracts;
using NCore.Nancy.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings.Contracts
{
    public class OrderLineDtoMapping : ClassMapping<OrderLineDto>
    {
        public OrderLineDtoMapping()
        {
            Table("OrderLine");
            this.MapEntityDto<OrderLineDtoMapping, OrderLineDto>();
            Property(m => m.Description);
            Property(m => m.OrderId);
            Property(m => m.Total);
            Property(m => m.Index, m => m.Column("[Index]"));
        }
    }
}