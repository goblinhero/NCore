using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Nancy.Commands;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class CustomerCreator : BaseCreator<Customer>
    {
        private readonly IPropertyHelper _propertyHelper;

        public CustomerCreator(IDictionary<string, object> dto)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            entity = new Customer();
            var setter = new DemoEntitySetter<Customer>(_propertyHelper,entity);
            setter.UpdateSimpleProperty(e => e.CompanyName);
            setter.PatchAddress(e => e.Address);
            return entity.IsValid(out errors);
        }
    }
}