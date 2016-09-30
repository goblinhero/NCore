using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Helpers;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class OrderLineUpdater : BaseUpdater<OrderLine>
    {
        private readonly OrderLineDto _dto;

        public OrderLineUpdater(long id, OrderLineDto dto)
            : base(id)
        {
            _dto = dto;
        }

        public override bool TryUpdate(OrderLine entity, ISession session, out IEnumerable<string> errors)
        {
            if (!Equals(entity.Index, _dto.Index))
            {
                var otherLines = session.QueryOver<OrderLine>()
                    .Where(ol => ol.Order == entity.Order)
                    .And(ol => ol.Id != entity.Id)
                    .List();
                new OrderLineIndexHandler().AdjustIndexes(entity, _dto.Index, otherLines);
            }
            entity.Total = _dto.Total;
            entity.Description = _dto.Description ?? string.Empty;
            return base.TryUpdate(entity, session, out errors);
        }
    }
}