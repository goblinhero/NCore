using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NCore.Nancy.Utilities;
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

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            var setter = new EntitySetter<OrderLine>(_propertyHelper,_entity);
            setter.UpdateSimpleProperty(e => e.Index,(o, n) =>
            {
                var otherLines = session.QueryOver<OrderLine>()
                    .Where(ol => ol.Order == _entity.Order)
                    .And(ol => ol.Id != _entity.Id)
                    .List();
                new OrderLineIndexHandler().AdjustIndexes(_entity, n, otherLines);
            });
            setter.UpdateSimpleProperty(e => e.Total);
            setter.UpdateSimpleProperty(e => e.Description);
            return this.Success(out errors);
        }
    }
}