using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class OrderMapping : ClassMapping<Order>
    {
        public OrderMapping()
        {
            this.MapEntity<OrderMapping, Order>();
            this.MapAddress<OrderMapping, Order>(c => c.Address);
            ManyToOne(m => m.Customer);
        }
    }
}