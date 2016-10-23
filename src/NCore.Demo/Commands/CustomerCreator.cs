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
    public class CustomerCreator : BaseCreator<Customer>
    {
        private readonly IPropertyHelper _propertyHelper;
        private readonly ICompanyContext _companyContext;
        public CustomerCreator(IDictionary<string, object> dto, ICompanyContext companyContext)
        {
            _companyContext = companyContext;
            _propertyHelper = new DictionaryHelper(dto);
        }

        protected override bool TryCreate(ISession session, out Customer entity, out IEnumerable<string> errors)
        {
            var company = _companyContext.CurrentCompany.HasValue
                ? session.Get<Company>(_companyContext.CurrentCompany.Value)
                : null;
            if (company == null)
            {
                entity = null;
                return company.NotFound(_companyContext.CurrentCompany, out errors);
            }
            entity = new Customer(company);
            var setter = new DemoEntitySetter<Customer>(_propertyHelper, entity);
            setter.UpdateSimpleProperty(e => e.CompanyName);
            setter.PatchAddress(e => e.Address);
            return entity.IsValid(out errors);
        }
    }
}