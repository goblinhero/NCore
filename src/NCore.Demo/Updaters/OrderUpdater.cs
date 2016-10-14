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
        private readonly object _dto;

        public OrderUpdater(long id, object dto)
            : base(id)
        {
            _dto = dto;
        }

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            //Update Customer
            //Update Address
            return base.TryUpdate(session, out errors);
        }
    }
}