using System.Collections.Generic;
using NHibernate;

namespace NCore.Web.Queries
{
    public interface IQuery<T>
    {
        bool TryExecute(IStatelessSession session, out T result, out IEnumerable<string> errors);
    }
}