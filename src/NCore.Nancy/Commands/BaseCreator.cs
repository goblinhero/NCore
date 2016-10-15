using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Commands
{
    public abstract class BaseCreator<T> : ICreator
        where T : IEntity
    {
        public long? AssignedId { get; set; }

        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            T entity;
            if (!TryCreate(session, out entity, out errors) || !entity.IsValid(out errors))
                return false;
            session.Save(entity);
            AssignedId = entity.Id;
            return this.Success(out errors);
        }

        protected abstract bool TryCreate(ISession session, out T entity, out IEnumerable<string> errors);
    }
}