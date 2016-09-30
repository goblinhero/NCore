using System;
using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Creators;
using NHibernate;

namespace NCore.Demo.Creators
{
    public class OrderCreator : BaseCreator<Order>
    {
        private readonly OrderDto _dto;

        public OrderCreator(OrderDto dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out Order entity, out IEnumerable<string> errors)
        {
            entity = new Order
            {
                Address = _dto.Address.ConvertToValueType(),
                CreationDate = DateTime.UtcNow,
                Customer = _dto.CustomerId.HasValue ? session.Get<Customer>(_dto.CustomerId.Value) : null
            };
            return entity.IsValid(out errors);
        }
    }
}