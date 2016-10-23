using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Web.Commands;
using NCore.Web.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class CompanyCreator : BaseCreator<Company>
    {
        private readonly IPropertyHelper _propertyHelper;

        public CompanyCreator(IDictionary<string, object> dto)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out Company entity, out IEnumerable<string> errors)
        {
            entity = new Company();
            var setter = new DemoEntitySetter<Company>(_propertyHelper, entity);
            setter.UpdateSimpleProperty(e => e.CompanyName);
            setter.PatchAddress(e => e.Address);
            return entity.IsValid(out errors);
        }
    }
}