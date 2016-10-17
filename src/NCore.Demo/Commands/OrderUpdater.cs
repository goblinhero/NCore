using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Web.Commands;
using NCore.Web.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class OrderUpdater : BaseUpdater<Order>
    {
        private readonly IPropertyHelper _propertyHelper;

        public OrderUpdater(long id, IDictionary<string, object> dto)
            : base(id)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TrySetProperties(ISession session, Order entity, out IEnumerable<string> errors)
        {
            var setter = new DemoEntitySetter<Order>(_propertyHelper, entity);
            setter.PatchAddress(o => o.Address);
            setter.UpdateComplexProperty(o => o.Customer, session);
            return this.Success(out errors);
        }
    }
}