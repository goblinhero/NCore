using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Demo.Utilities;
using NCore.Nancy.Creators;
using NCore.Nancy.Updaters;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Creators
{
    public class CustomerCreator : BaseCreator<Customer>
    {
        private readonly IPropertyHelper _propertyHelper;

        public CustomerCreator(IDictionary<string, object> dto)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        public override bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            entity = new Customer();
            var setter = new DemoEntitySetter<Customer>(_propertyHelper,entity);
            setter.UpdateSimpleProperty(e => e.CompanyName);
            setter.PatchAddress(e => e.Address);
            return entity.IsValid(out errors);
        }
    }
}