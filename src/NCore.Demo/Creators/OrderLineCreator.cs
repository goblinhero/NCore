using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Helpers;
using NCore.Extensions;
using NCore.Nancy.Creators;
using NHibernate;

namespace NCore.Demo.Creators
{
    public class OrderLineCreator : BaseCreator<OrderLine>
    {
        private readonly object _dto;

        public OrderLineCreator(object dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out OrderLine entity, out IEnumerable<string> errors)
        {
            long orderId;
            Order order = null;
            if (!TryGetValueType("OrderId", _dto, out orderId))
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
            _entity = new OrderLine(order);
            UpdateSimpleProperty(ol => ol.Description,_dto);
            int? index;
            TryGetValueType("Index", _dto, out index);
            new OrderLineIndexHandler().AdjustIndexes(_entity, index, existingLines);
            entity = _entity;
            return this.Success(out errors);
        }
    }
}