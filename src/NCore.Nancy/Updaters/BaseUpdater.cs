using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Updaters
{
    public abstract class BaseUpdater<TEntity> : IUpdater<TEntity>
        where TEntity : IEntity
    {
        protected TEntity _entity;

        protected BaseUpdater(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public void SetEntity(TEntity entity)
        {
            _entity = entity;
        }

        public bool TryUpdate(ISession session, out IEnumerable<string> errors)
        {
            if (!TrySetProperties(session, out errors))
                return false;
            return _entity.IsValid(out errors);
        }

        protected abstract bool TrySetProperties(ISession session, out IEnumerable<string> errors);
    }
}