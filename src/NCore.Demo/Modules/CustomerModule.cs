using System.Collections.Generic;
using System.Linq.Expressions;
using Elasticsearch.Net;
using NCore.Demo.Commands;
using NCore.Demo.Contracts;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Strategies;
using NCore.Web;
using NCore.Web.Api;
using NCore.Web.Commands;

namespace NCore.Demo.Modules
{
    public class CustomerModule : HasCompanyModule<Customer, CustomerDto>
    {
        public CustomerModule(ICompanyContext companyContext)
            : base(companyContext)
        {
            Get[_staticRoutes.Base] = p => GetList();
        }

        private object GetList()
        {
            IEnumerable<string> errors;
            IList<CustomerDto> result;
            var query = Request.Query["query"];
            return new CompositeCustomerFreeTextQuery(_companyContext, query).TryExecute(out result, out errors) || _sessionHelper.TryQuery(new FindCustomerQuery(), out result, out errors)
                ? new
                {
                    Success = true,
                    Result = result
                }
                : (object)new
                {
                    Success = false,
                    Errors = errors
                };
        }

        protected override ICreator GetCreator(IDictionary<string, object> dto)
        {
            return new CustomerCreator(dto, _companyContext);
        }

        protected override ICommand GetUpdater(long id, IDictionary<string, object> dto)
        {
            return new CustomerUpdater(id, dto);
        }

        protected override ICommand GetDeleter(long id)
        {
            return new CustomerDeleter(id);
        }
    }
}