using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Updaters
{
    public abstract class BaseUpdater<T> : IUpdater<T>
        where T : IEntity
    {
        protected BaseUpdater(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public virtual bool TryUpdate(T entity, ISession session, out IEnumerable<string> errors)
        {
            return entity.IsValid(out errors);
        }
    }
}