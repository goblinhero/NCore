using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Creators
{
    public interface ICreator<T>
    {
        long? AssignedId { get; set; }
        bool TryCreate(ISession session, out T entity, out IEnumerable<string> errors);
    }
}