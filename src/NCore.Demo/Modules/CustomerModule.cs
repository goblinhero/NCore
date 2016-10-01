using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Demo.Creators;
using NCore.Demo.Deleters;
using NCore.Demo.Domain;
using NCore.Demo.Queries;
using NCore.Demo.Updaters;
using NCore.Nancy.Api;
using NCore.Nancy.Creators;
using NCore.Nancy.Deleters;
using NCore.Nancy.Updaters;

namespace NCore.Demo.Modules
{
    public class CustomerModule : CRUDModule<Customer, CustomerDto>
    {
        public CustomerModule()
        {
            Get[_staticRoutes.Base] = p => GetList();
        }

        private object GetList()
        {
            IEnumerable<string> errors;
            IList<CustomerDto> result;
            return _sessionHelper.TryQuery(new FindCustomerQuery(), out result, out errors)
                ? new
                {
                    Success = true,
                    Result = result
                }
                : (object) new
                {
                    Success = false,
                    Errors = errors
                };
        }

        protected override ICreator<Customer> GetCreator(CustomerDto dto)
        {
            return new CustomerCreator(dto);
        }

        protected override IUpdater<Customer> GetUpdater(long id, CustomerDto dto)
        {
            return new CustomerUpdater(id, dto);
        }

        protected override IDeleter<Customer> GetDeleter(long id)
        {
            return new CustomerDeleter(id);
        }
    }
}