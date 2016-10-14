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
        private readonly object _dto;

        public OrderCreator(object dto)
        {
            _dto = dto;
        }

        public override bool TryCreate(ISession session, out Order entity, out IEnumerable<string> errors)
        {
            entity = new Order();
            //Update Customer
            //Update Address
            return entity.IsValid(out errors);
        }
    }
}