using System.Collections.Generic;
using NCore.Nancy.Updaters;
using NHibernate;

namespace NCore.Nancy.Creators
{
    public abstract class BaseCreator<T> : ICreator<T> 
        where T : IEntity
    {
        public long? AssignedId { get; set; }
        public abstract bool TryCreate(ISession session, out T entity, out IEnumerable<string> errors);
    }
}