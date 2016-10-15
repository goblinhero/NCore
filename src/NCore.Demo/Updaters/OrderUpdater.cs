using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Extensions;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Nancy.Updaters;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Updaters
{
    public class OrderUpdater : BaseUpdater<Order>
    {
        private IPropertyHelper _propertyHelper;

        public OrderUpdater(long id, IDictionary<string, object> dto)
            : base(id)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TrySetProperties(ISession session, out IEnumerable<string> errors)
        {
            var setter = new DemoEntitySetter<Order>(_propertyHelper, _entity);
            setter.PatchAddress(o => o.Address);
            setter.UpdateComplexProperty(o => o.Customer,session);
            return this.Success(out errors);
        }
    }
}