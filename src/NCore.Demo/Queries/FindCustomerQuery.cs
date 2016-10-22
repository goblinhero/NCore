using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Extensions;
using NCore.Web.Queries;
using NHibernate;

namespace NCore.Demo.Queries
{
    public class FindCustomerQuery : IQuery<IList<CustomerDto>>
    {
        public bool TryExecute(ISession session, out IList<CustomerDto> result, out IEnumerable<string> errors)
        {
            result = session.QueryOver<CustomerDto>().List();
            return this.Success(out errors);
        }
    }
}