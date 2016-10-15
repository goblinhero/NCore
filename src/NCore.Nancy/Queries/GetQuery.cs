using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Nancy.Queries
{
    public class GetQuery<T> : IQuery<T>
    {
        private readonly long _id;

        public GetQuery(long id)
        {
            _id = id;
        }

        public bool TryExecute(IStatelessSession session, out T result, out IEnumerable<string> errors)
        {
            result = session.Get<T>(_id);
            if (result == null)
                return result.NotFound(_id, out errors);
            return this.Success(out errors);
        }
    }
}