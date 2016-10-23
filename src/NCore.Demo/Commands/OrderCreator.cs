using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Demo.Utilities;
using NCore.Extensions;
using NCore.Web;
using NCore.Web.Commands;
using NCore.Web.Utilities;
using NHibernate;

namespace NCore.Demo.Commands
{
    public class OrderCreator : BaseCreator<Order>
    {
        private readonly IPropertyHelper _propertyHelper;
        private readonly ICompanyContext _companyContext;

        public OrderCreator(IDictionary<string, object> dto, ICompanyContext companyContext)
        {
            _companyContext = companyContext;
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out Order entity, out IEnumerable<string> errors)
        {
            var company = _companyContext.CurrentCompany.HasValue
                  ? session.Get<Company>(_companyContext.CurrentCompany.Value)
                  : null;
            if (company == null)
            {
                entity = null;
                return company.NotFound(_companyContext.CurrentCompany, out errors);
            }
            entity = new Order(company);
            var setter = new DemoEntitySetter<Order>(_propertyHelper, entity);
            setter.PatchAddress(e => e.Address);
            setter.UpdateComplexProperty(o => o.Customer, session);
            return entity.IsValid(out errors);
        }
    }
}