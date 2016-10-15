using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Nancy.Commands;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class CustomerUpdater : BaseUpdater<Customer>
    {
        private readonly IPropertyHelper _propertyHelper;

        public CustomerUpdater(long id, IDictionary<string, object> dto)
            : base(id)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            var setter = new DemoEntitySetter<Customer>(_propertyHelper,_entity);
            setter.UpdateSimpleProperty(e => e.CompanyName);
            setter.PatchAddress(e => e.Address);
            return this.Success(out errors);
        }
    }
}