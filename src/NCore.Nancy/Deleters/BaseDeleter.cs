using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Deleters
{
    public class BaseDeleter<T> : IDeleter<T>
    {
        public BaseDeleter(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public virtual bool TryDelete(T entity, ISession session, out IEnumerable<string> errors)
        {
            session.Delete(entity);
            return this.Success(out errors);
        }
    }
}