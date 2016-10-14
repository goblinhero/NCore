using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Extensions;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class CustomerUpdater : BaseUpdater<Customer>
    {
        private readonly object _dto;

        public CustomerUpdater(long id, object dto)
            : base(id)
        {
            _dto = dto;
        }

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            UpdateSimpleProperty(e => e.CompanyName, _dto);
            //Update Address
            return this.Success(out errors);
        }
    }
}