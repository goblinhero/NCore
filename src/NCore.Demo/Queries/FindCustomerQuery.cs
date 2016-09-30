using System.Collections.Generic;
using NCore.Demo.Domain;
using NCore.Extensions;
using NCore.Nancy.Queries;
using NHibernate;

namespace NCore.Demo.Queries
{
    public class FindCustomerQuery : IQuery<IList<Customer>>
    {
        public bool TryExecute(ISession session, out IList<Customer> result, out IEnumerable<string> errors)
        {
            result = session.QueryOver<Customer>().List();
            return this.Success(out errors);
        }
    }
}