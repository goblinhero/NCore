using System.Collections.Generic;
using NHibernate;

namespace NCore.Nancy.Deleters
{
    public interface IDeleter<T>
    {
        long Id { get; }
        bool TryDelete(T entity, ISession session, out IEnumerable<string> errors);
    }
}