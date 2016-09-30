using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Updaters
{
    public interface IUpdater<T>
    {
        long Id { get; }
        bool TryUpdate(T entity, ISession session, out IEnumerable<string> errors);
    }
}