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
        private readonly OrderLineDto _dto;

        public OrderLineCreator(OrderLineDto dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out OrderLine entity, out IEnumerable<string> errors)
        {
            var order = session.Get<Order>(_dto.OrderId);
            if (order == null)
            {
                entity = null;
                return order.NotFound(_dto.OrderId, out errors);
            }
            var existingLines = session.QueryOver<OrderLine>().Where(ol => ol.Order == order).List();
            entity = new OrderLine(order)
            {
                Description = _dto.Description ?? string.Empty
            };
            new OrderLineIndexHandler().AdjustIndexes(entity, _dto.Index, existingLines);
            return this.Success(out errors);
        }
    }
}