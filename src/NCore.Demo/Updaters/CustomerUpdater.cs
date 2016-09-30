using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class CustomerUpdater : BaseUpdater<Customer>
    {
        private readonly CustomerDto _dto;

        public CustomerUpdater(long id, CustomerDto dto)
            : base(id)
        {
            _dto = dto;
        }

        public override bool TryUpdate(Customer entity, ISession session, out IEnumerable<string> errors)
        {
            entity.Address = _dto.Address.ConvertToValueType();
            entity.CompanyName = _dto.CompanyName;

            return base.TryUpdate(entity, session, out errors);
        }
    }
}