using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Commands
{
    public abstract class BaseUpdater<TEntity> : ICommand
        where TEntity : IEntity
    {
        private readonly long _id;
        protected TEntity _entity;

        protected BaseUpdater(long id)
        {
            _id = id;
        }

        protected abstract bool TrySetProperties(ISession session, out IEnumerable<string> errors);
        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            _entity = session.Get<TEntity>(_id);
            if (_entity == null)
            {
                return _entity.NotFound(_id, out errors);
            }
            return TrySetProperties(session, out errors) && 
                _entity.IsValid(out errors);
        }
    }
}