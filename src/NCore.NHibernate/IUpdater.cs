using System.Collections.Generic;
using NHibernate;

namespace NCore.NHibernate
{
    public interface IUpdater<T>
    {
        long Id { get; }
        bool TryUpdate(T entity, ISession session, out IEnumerable<string> errors);
    }
}