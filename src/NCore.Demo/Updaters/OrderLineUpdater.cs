using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Helpers;
using NCore.Extensions;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class OrderLineUpdater : BaseUpdater<OrderLine>
    {
        private readonly object _dto;

        public OrderLineUpdater(long id, object dto)
            : base(id)
        {
            _dto = dto;
        }

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            UpdateValueTypeProperty(e => e.Index,_dto,(o, n) =>
            {
                var otherLines = session.QueryOver<OrderLine>()
                    .Where(ol => ol.Order == _entity.Order)
                    .And(ol => ol.Id != _entity.Id)
                    .List();
                new OrderLineIndexHandler().AdjustIndexes(_entity, n, otherLines);
            });
            UpdateValueTypeProperty(e => e.Total,_dto);
            UpdateSimpleProperty(e => e.Description,_dto);
            return this.Success(out errors);
        }
    }
}