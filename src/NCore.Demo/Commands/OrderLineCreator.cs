using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class OrderLineCreator : BaseCreator<OrderLine>
    {
        private readonly IPropertyHelper _propertyHelper;

        public OrderLineCreator(IDictionary<string, object> dto)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out OrderLine entity, out IEnumerable<string> errors)
        {
            long orderId;
            Order order = null;
            if (!_propertyHelper.TryGetValue("OrderId", out orderId))
            {
                entity = null;
                return order.NotFound(orderId, out errors);
            }
            order = session.Get<Order>(orderId);
            if (order == null)
            {
                entity = null;
                return order.NotFound(orderId, out errors);
            }
            var existingLines = session.QueryOver<OrderLine>().Where(ol => ol.Order == order).List();
            entity = new OrderLine(order);
            var setter = new EntitySetter<OrderLine>(_propertyHelper, entity);
            setter.UpdateSimpleProperty(ol => ol.Description);

            int? index;
            _propertyHelper.TryGetValue("Index", out index);
            new OrderLineIndexHandler().AdjustIndexes(entity, index, existingLines);
            return this.Success(out errors);
        }
    }
}