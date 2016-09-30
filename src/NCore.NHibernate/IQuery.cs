using System.Collections.Generic;
using NHibernate;

namespace NCore.NHibernate
{
    public interface IQuery<T>
    {
        bool TryExecute(ISession session, out T result, out IEnumerable<string> errors);
    }
}