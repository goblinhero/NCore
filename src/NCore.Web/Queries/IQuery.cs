using System.Collections.Generic;
using NHibernate;

namespace NCore.Web.Queries
{
    public interface IQuery<T>
    {
        bool TryExecute(ISession session, out T result, out IEnumerable<string> errors);
    }
}