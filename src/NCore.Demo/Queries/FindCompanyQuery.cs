using System.Collections.Generic;
using NCore.Demo.Contracts;
using NCore.Extensions;
using NCore.Web.Queries;
using NHibernate;

namespace NCore.Demo.Queries
{
    public class FindCompanyQuery : IQuery<IList<CompanyDto>>
    {
        public bool TryExecute(ISession session, out IList<CompanyDto> result, out IEnumerable<string> errors)
        {
            result = session.QueryOver<CompanyDto>().List();
            return this.Success(out errors);
        }
    }
}