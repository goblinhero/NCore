using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Nancy.Commands;
using NCore.Nancy.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class OrderCreator : BaseCreator<Order>
    {
        private readonly IPropertyHelper _propertyHelper;

        public OrderCreator(IDictionary<string, object> dto)
        {
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out Order entity, out IEnumerable<string> errors)
        {
            entity = new Order();
            var setter = new DemoEntitySetter<Order>(_propertyHelper, entity);
            setter.PatchAddress(e => e.Address);
            setter.UpdateComplexProperty(o => o.Customer, session);
            return entity.IsValid(out errors);
        }
    }
}