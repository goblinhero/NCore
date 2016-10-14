using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Updaters
{
    public interface IUpdater<T>
    {
        long Id { get; }
        void SetEntity(T entity);
        bool TryUpdate(ISession session, out IEnumerable<string> errors);
    }
}