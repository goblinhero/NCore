using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Web.Extensions;
using NHibernate.Mapping.ByCode.Conformist;

namespace NCore.Demo.Mappings
{
    public class OrderLineMapping : ClassMapping<OrderLine>
    {
        public OrderLineMapping()
        {
            this.MapEntity<OrderLineMapping, OrderLine>();
            this.MapDemoCompany<OrderLineMapping, OrderLine>();
            Property(m => m.Description, m => m.NotNullable(true));
            Property(m => m.Index, m => m.Column("[Index]"));
            Property(m => m.Total);
            ManyToOne(m => m.Order);
        }
    }
}