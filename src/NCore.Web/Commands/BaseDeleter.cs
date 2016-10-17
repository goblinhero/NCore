using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Web.Commands
{
    public class BaseDeleter<T> : ICommand
    {
        private readonly long _id;

        public BaseDeleter(long id)
        {
            _id = id;
        }

        public bool TryExecute(ISession session, out IEnumerable<string> errors)
        {
            var entity = session.Get<T>(_id);
            if (entity == null)
            {
                return entity.NotFound(_id, out errors);
            }
            return TryDelete(entity, session, out errors);
        }

        public virtual bool TryDelete(T entity, ISession session, out IEnumerable<string> errors)
        {
            session.Delete(entity);
            return this.Success(out errors);
        }
    }
}