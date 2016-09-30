using System.Collections.Generic;
using NHibernate;

namespace NCore.NHibernate
{
    public interface ICreator<T>
    {
        long? AssignedId { get; set; }
        bool TryCreate(ISession session, out T entity, out IEnumerable<string> errors);
    }
}