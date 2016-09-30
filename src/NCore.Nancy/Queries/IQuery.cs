using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Queries
{
    public interface IQuery<T>
    {
        bool TryExecute(ISession session, out T result, out IEnumerable<string> errors);
    }
}