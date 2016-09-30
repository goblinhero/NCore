using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Creators
{
    public abstract class BaseCreator<T> : ICreator<T>
    {
        public long? AssignedId { get; set; }
        public abstract bool TryCreate(ISession session, out T entity, out IEnumerable<string> errors);
    }
}