using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Web.Commands;
using NCore.Web.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class OrderLineUpdater : BaseUpdater<OrderLine>
    {
        private readonly IPropertyHelper _propertyHelper;

        public OrderLineUpdater(long id, IDictionary<string, object> dto)
            : base(id)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TrySetProperties(ISession session, OrderLine entity, out IEnumerable<string> errors)
        {
            var setter = new EntitySetter<OrderLine>(_propertyHelper, entity);
            setter.UpdateSimpleProperty(e => e.Index, (o, n) =>
            {
                var otherLines = session.QueryOver<OrderLine>()
                    .Where(ol => ol.Order == entity.Order)
                    .And(ol => ol.Id != entity.Id)
                    .List();
                new OrderLineIndexHandler().AdjustIndexes(entity, n, otherLines);
            });
            setter.UpdateSimpleProperty(e => e.Total);
            setter.UpdateSimpleProperty(e => e.Description);
            return this.Success(out errors);
        }
    }
}