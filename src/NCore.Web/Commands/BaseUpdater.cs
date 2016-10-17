using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Web.Commands
{
    public abstract class BaseUpdater<TEntity> : ICommand
        where TEntity : IEntity
    {
        private readonly long _id;

        protected BaseUpdater(long id)
        {
            _id = id;
        }

        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            var entity = session.Get<TEntity>(_id);
            if (entity == null)
            {
                return entity.NotFound(_id, out errors);
            }
            return TrySetProperties(session, entity, out errors) &&
                   entity.IsValid(out errors);
        }

        protected abstract bool TrySetProperties(ISession session, TEntity entity, out IEnumerable<string> errors);
    }
}