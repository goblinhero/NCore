using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class OrderUpdater : BaseUpdater<Order>
    {
        private readonly OrderDto _dto;

        public OrderUpdater(long id, OrderDto dto)
            : base(id)
        {
            _dto = dto;
        }

        public override bool TryUpdate(Order entity, ISession session, out IEnumerable<string> errors)
        {
            entity.Address = _dto.Address.ConvertToValueType();
            entity.Customer = _dto.CustomerId.HasValue ? session.Get<Customer>(_dto.CustomerId.Value) : null;
            return base.TryUpdate(entity, session, out errors);
        }
    }
}