using System.Collections.Generic;
using NCore.Extensions;
using NHibernate;

namespace NCore.Web.Queries
{
    public class GetQuery<T> : IQuery<T>
    {
        private readonly long _id;

        public GetQuery(long id)
        {
            _id = id;
        }

        public bool TryExecute(ISession session, out T result, out IEnumerable<string> errors)
        {
            result = session.Get<T>(_id);
            return result == null 
                ? result.NotFound(_id, out errors) 
                : this.Success(out errors);
        }
    }
}